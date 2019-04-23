using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MessageBrokers.Notifications
{
    public class Command : INotification
    {
        public string Message { get; set; }
    }

    public class CommandHandler : INotificationHandler<Command>
    {
        public Task Handle(Command notification, CancellationToken cancellationToken) =>
             new Task(() => Console.WriteLine(notification.Message));
    }
}
