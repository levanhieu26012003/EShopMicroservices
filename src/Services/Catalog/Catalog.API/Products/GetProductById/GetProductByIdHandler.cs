
namespace Catalog.API.Products.GetProductById;
public record GetProductByIdResult(Product Product);
public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;
public class GetProductByIdHandler(IDocumentSession session, ILogger<GetProductByIdHandler> logger) : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
{
    public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("Get Product By Id Query Handler . Handle call with {@Query}", query);
        var product = await session.LoadAsync<Product>(query.Id, cancellationToken);
            // dùng var (tiện lợi khi code) khác với dymnamic, gắn ngay compile time nên ko ảnh hưởng đến hiệu năng, 
        if(product is null)
        {
            throw new ProductNotFoundException();
        }
        
        return new GetProductByIdResult(product);
    }
}
