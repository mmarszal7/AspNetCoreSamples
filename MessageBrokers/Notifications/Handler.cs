using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MessageBrokers.Notifications
{
    public class Handler : INotificationHandler<Event>
    {
        public Task Handle(Event notification, CancellationToken cancellationToken) =>
             new Task(() => Console.WriteLine(notification.Message));
    }
}
