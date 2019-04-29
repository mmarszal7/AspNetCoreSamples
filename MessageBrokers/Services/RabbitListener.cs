using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace MessageBrokers.Services
{
    public class RabbitListener
    {
        ConnectionFactory Factory { get; set; }
        IConnection Connection { get; set; }
        IModel Channel { get; set; }

        public RabbitListener(string hostName)
        {
            Factory = new ConnectionFactory() { HostName = hostName };
            Connection = Factory.CreateConnection();
            Channel = Connection.CreateModel();
        }

        public void Register()
        {
            Channel.QueueDeclare(queue: "demoQueue", durable: false, exclusive: false, autoDelete: false, arguments: null);

            var consumer = new EventingBasicConsumer(Channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                var message = Encoding.UTF8.GetString(body);
            };
            Channel.BasicConsume(queue: "demoQueue", autoAck: true, consumer: consumer);
        }

        public void Deregister()
        {
            Connection.Close();
        }
    }
}
