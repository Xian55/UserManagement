using System.Diagnostics;

using MediatR;

using Microsoft.Extensions.Logging;

using UserManagement.Domain.Primitives.Result;

namespace UserManagement.Application.Behaviors
{
    public sealed class LoggingBehavior<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : Result
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> logger;

        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        {
            this.logger = logger;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            logger.LogInformation(
                "Request started   {@RequestName}",
                typeof(TRequest).Name);

            long start = Stopwatch.GetTimestamp();

            var result = await next();

            if (result.IsFailure)
            {
                logger.LogError(
                    "Request failed    {@RequestName}, {@Error}",
                    typeof(TRequest).Name,
                    result.Error);
            }

            logger.LogInformation(
                "Request completed {@RequestName} {@Elapsed}ms",
                typeof(TRequest).Name,
                (Stopwatch.GetElapsedTime(start).TotalMicroseconds));

            return result;
        }
    }
}
