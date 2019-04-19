using MediatR;

namespace MessageBrokers.Notifications
{
    public class Event : INotification
    {
        public string Message { get; set; }
    }
}
