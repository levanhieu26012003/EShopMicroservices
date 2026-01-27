namespace Basket.API.Basket.Storebasket;
public record StoreBasketResult(string UserName);
public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;
public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
{
    public StoreBasketCommandValidator()
    {
        RuleFor(x => x.Cart).NotEmpty().WithMessage("Cart can not be null");
        RuleFor(x => x.Cart.UserName).NotEmpty().WithMessage("Name is required");
    }
}
public class StorebasketHandler(IBasketRepository repository) : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
    {
        ShoppingCart cart = command.Cart;

        var basket = await repository.StoreBasket(cart, cancellationToken);

        return new StoreBasketResult(basket.UserName);
    }
}
