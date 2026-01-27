using FluentValidation;

namespace Basket.API.Basket.DeleteBasket;
public record DeleteBasketResult(bool IsSuccess);
public record DeleteBasketCommand(string UserName): ICommand<DeleteBasketResult>;
public class DeleteBasketCommandValidator : AbstractValidator<DeleteBasketCommand>
{
    public DeleteBasketCommandValidator()
    {
        RuleFor(x => x.UserName).NotEmpty().WithMessage("UserName is required");
    }
}
public class DeleteBasketHandler(IBasketRepository repository) : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
{
    public async Task<DeleteBasketResult> Handle(DeleteBasketCommand request, CancellationToken cancellationToken)
    {
        var result = await repository.DeleteBasket(request.UserName, cancellationToken);
        return new DeleteBasketResult(result);
    }
}
