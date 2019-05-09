using MediatR;
using MessageBrokers.Services;
using System.Threading;
using System.Threading.Tasks;

namespace MessageBrokers.Notifications
{
    public class SendMessageToQueueCommand : INotification
    {
        public string QueueName { get; set; }
        public string Message { get; set; }
    }

    public class SendMessageToQueueCommandHandler : INotificationHandler<SendMessageToQueueCommand>
    {
        private readonly RabbitListener MessageBroker;

        public SendMessageToQueueCommandHandler(RabbitListener messageBroker)
        {
            MessageBroker = messageBroker;
        }

        public Task Handle(SendMessageToQueueCommand command, CancellationToken cancellationToken) =>
            Task.Run(() => MessageBroker.Send(command.QueueName, command.Message));
    }
}
