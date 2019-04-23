using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace MessageBrokers.Notifications
{
    public class Query : IRequest<Result>
    {
        public string Id { get; set; }
    }

    public class QueryHandler : IRequestHandler<Query, Result>
    {
        public Task<Result> Handle(Query notification, CancellationToken cancellationToken) =>
             Task.FromResult(new Result() { Message = "Test message" });
    }

    public class Result
    {
        public string Message { get; set; }
    }
}
