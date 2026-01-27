
namespace Catalog.API.Products.GetProductById;
public record GetProductByIdResult(Product Product);
public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;
public class GetProductByIdHandler(IDocumentSession session) : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
{
    public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        var product = await session.LoadAsync<Product>(query.Id, cancellationToken);
            // dùng var (tiện lợi khi code) khác với dymnamic, gắn ngay compile time nên ko ảnh hưởng đến hiệu năng, 
        if(product is null)
        {
            throw new ProductNotFoundException(product.Id);
        }
        
        return new GetProductByIdResult(product);
    }
}
