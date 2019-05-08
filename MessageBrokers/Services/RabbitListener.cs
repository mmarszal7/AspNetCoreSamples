using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace MessageBrokers.Services
{
    public class RabbitListener
    {
        ConnectionFactory Factory { get; set; }
        IConnection Connection { get; set; }
        IModel Model { get; set; }

        public RabbitListener(string hostName)
        {
            Factory = new ConnectionFactory() { HostName = hostName, Port = 5672, UserName = "guest", Password = "guest" };
            Connection = Factory.CreateConnection();
            Model = Connection.CreateModel();
        }

        public void Register(string queueName, Action<string> callback)
        {
            Model.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

            var consumer = new EventingBasicConsumer(Model);
            Model.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);

            consumer.Received += (s, e) =>
            {
                var body = e.Body;
                var message = Encoding.UTF8.GetString(body);
                callback(message);
                //Model.BasicAck(e.DeliveryTag, multiple: false);
            };
        }

        public void Send(string queueName, string message)
        {
            byte[] messageBuffer = Encoding.Default.GetBytes(message);
            var properties = Model.CreateBasicProperties();
            Model.BasicPublish("", queueName, properties, messageBuffer);
        }
    }
}
