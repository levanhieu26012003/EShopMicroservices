using Discount.Grpc;

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
public class StorebasketHandler
    (IBasketRepository repository, DiscountProService.DiscountProServiceClient discountPro) 
    : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
    {
        ShoppingCart cart = command.Cart;

        await DeductDiscount(cart, cancellationToken);

        var basket = await repository.StoreBasket(cart, cancellationToken);

        return new StoreBasketResult(basket.UserName);
    }

    private async Task  DeductDiscount(ShoppingCart cart, CancellationToken cancellationToken)
    {
        foreach(var item in cart.Items)
        {
            var coupon = await discountPro.GetDiscountAsync(new GetDiscountRequest { ProductName = item.ProductName }, cancellationToken: cancellationToken);
            item.Price -= coupon.Amount;
        }
    }
}
