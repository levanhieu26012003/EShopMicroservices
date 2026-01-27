
namespace Catalog.API.Products.UpdateProduct;
public record UpdateProductResult(bool IsSuccess);
public record UpdateProductCommand(Guid Id, string Name, List<string> Category, string Decription, string ImageFile, decimal Price)
    : ICommand<UpdateProductResult>;
public class UpdateProductHandler(IDocumentSession session)
    : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {

        var product = await session.LoadAsync<Product>(command.Id, cancellationToken);

        if (product is null) {
            throw new ProductNotFoundException(product.Id);
        }

        product.Name = command.Name;
        product.Category = command.Category;
        product.Description = command.Decription;
        product.ImageFile = command.ImageFile;
        product.Price = command.Price;

        session.Update(product);
        await session.SaveChangesAsync(cancellationToken);

        return new UpdateProductResult(true);
    }
}
