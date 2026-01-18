
namespace Catalog.API.Products.GetProducts;
public record GetProductsResult(IEnumerable<Product> Products); // tạo kiểu dữ liệu trả về,  đóng vai trò như DTO
//tại sao tạo recode
//    + tính mở rộng , vd thêm int TotalCount
//    + tính rõ ràng khi đọc tên recode
//    + phù hợp với CQRS bởi mỗi luồng sẽ trả về 1 response riêng

public record GetProductsQuery() : IQuery<GetProductsResult>; // tạo 
internal class GetProductQueryHandler(IDocumentSession session, // quản lý session làm việc với postgreSQL
    ILogger<GetProductQueryHandler> logger) // truyền tên vào để gắn nhãn, tiện cho tìm kiếm khi trong file log
    : IQueryHandler<GetProductsQuery, GetProductsResult> // kế thừa truyền tham số nhằm an toàn về dữ liệu và rõ ràng mục đích
{
    // được maping và được MediaR tìm khi sender gọi
    public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("Get Products Query Handler . Handle call with {@Query}", query);
        var products = await session.Query<Product>().ToListAsync(cancellationToken);
        return new GetProductsResult(products);
    }
}
