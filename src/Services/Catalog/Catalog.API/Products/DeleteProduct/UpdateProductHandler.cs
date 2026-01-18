
namespace Catalog.API.Products.DeleteProduct;
public record DeleteProductResult(bool IsSuccess);
public record DeleteProductCommand(Guid Id)
    : ICommand<DeleteProductResult>;
public class DeleteProductHandler(IDocumentSession session, ILogger<DeleteProductHandler> logger)
    : ICommandHandler<DeleteProductCommand, DeleteProductResult>
{
    public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("Delete Product Command Handler . Handle call with {@Command}", command);
        session.Delete<Product>(command.Id);
        await session.SaveChangesAsync();

        return new DeleteProductResult(true);
    }
}
