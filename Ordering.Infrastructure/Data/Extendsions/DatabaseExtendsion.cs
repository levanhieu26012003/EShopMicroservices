using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Ordering.Infrastructure.Data.Extendsions;
// chạy tự động migration trên docker
public static class DatabaseExtendsion
{
    public static async Task InitialiseDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope(); // sử dụng tạo 1 ApplicationDbContext từ scope thủ công để ko rò rỉ bộ nhớ
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        context.Database.MigrateAsync().GetAwaiter().GetResult();

        await SeedAsync(context);
    }

    private static async Task SeedAsync(ApplicationDbContext context)
    {
        await SeedCustomerAsync(context);
        await SeedProductAsync(context);
        await SeedOrderAsync(context);
    }

    private static async Task SeedCustomerAsync(ApplicationDbContext context)
    {
        if (!await context.Customers.AnyAsync())
        {
            await context.AddRangeAsync(InitialData.Customers);
            await context.SaveChangesAsync();
        }
    }

    private static async Task SeedProductAsync(ApplicationDbContext context)
    {
        if (!await context.Prodcuts.AnyAsync())
        {
            await context.AddRangeAsync(InitialData.Products);
            await context.SaveChangesAsync();
        }
    }

    private static async Task SeedOrderAsync(ApplicationDbContext context)
    {
        if (!await context.Orders.AnyAsync())
        {
            await context.AddRangeAsync(InitialData.OrdersWithItems);
            await context.SaveChangesAsync();
        }
    }
}
