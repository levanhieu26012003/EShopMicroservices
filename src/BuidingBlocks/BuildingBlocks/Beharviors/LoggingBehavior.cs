using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace BuildingBlocks.Beharviors;
public class LoggingBehavior<TRequest, TResponse> // yêu cầu đầu vào đâu ra để chuẩn hóa dữ liệu
    (ILogger<LoggingBehavior<TRequest, TResponse>> logger) 
    : IPipelineBehavior<TRequest, TResponse> 
    where TRequest : notnull, IRequest<TResponse> // request phải có yêu cầu đầu ra rõ ràng trùng với propety được truyền
    where TResponse : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        logger.LogInformation("[START] Handle request={Request} - Response={Response} - RequestData={RequestData}",
            typeof(TRequest).Name, typeof(TResponse).Name, request);

        var timer = new Stopwatch();
        timer.Start();

        var response = await next(); // handle thực sự chạy

        timer.Stop();
        var timeTaken = timer.Elapsed;
        if (timeTaken.Seconds > 3)
        {
            logger.LogWarning("[PERFORMENCE] The request {Request} took {timeTaken} seconds",
                typeof(TRequest).Name, timeTaken.Seconds);
        }
        logger.LogInformation("[END] Handle {Request} with {Respone}"
            ,typeof(TRequest).Name, typeof(TResponse).Name);
        return response;
    }
}
