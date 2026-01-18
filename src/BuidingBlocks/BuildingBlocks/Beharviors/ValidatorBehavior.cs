
using BuildingBlocks.CQRS;
using FluentValidation;
using MediatR;

namespace BuildingBlocks.Beharviors;

public class ValidatorBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators) // list vì có nhiều rule
    :IPipelineBehavior<TRequest, TResponse>
    where TRequest : ICommand<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequest>(request); // nhận biết đối tượng áp dụng rule

        var validationResults = await Task.WhenAll(validators.Select(v => v.ValidateAsync(context, cancellationToken)));

        var failtures = validationResults
            .Where(r => r.Errors.Any()) // lọc rule nào có lỗi
            .SelectMany(r => r.Errors) // lấy toàn bộ lỗi của mỗi kết quả
            .ToList();

        if (failtures.Any())
        {
            throw new ValidationException(failtures);
        }

        return await next();
    }
}
