using MediatR;
using MessageBrokers.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MessageBrokers.Notifications
{
    public class SubscribeOnQueueCommand : INotification
    {
        public string QueueName { get; set; }
        public Action<string> Callback { get; set; }
    }

    public class SubscribeOnQueueCommandHandler : INotificationHandler<SubscribeOnQueueCommand>
    {
        private readonly RabbitListener MessageBroker;

        public SubscribeOnQueueCommandHandler(RabbitListener messageBroker)
        {
            MessageBroker = messageBroker;
        }

        public Task Handle(SubscribeOnQueueCommand command, CancellationToken cancellationToken) =>
            Task.Run(() => MessageBroker.Register(command.QueueName, command.Callback));
    }
}
