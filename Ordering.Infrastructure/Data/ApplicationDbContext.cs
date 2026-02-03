using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Ordering.Domain.Models;

namespace Ordering.Infrastructure.Data;
public class ApplicationDbContext : DbContext
{
    // nhận cấu hình từ Program như Provider, Connection string, Logg, Interceptor
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }
    public DbSet<Order> Orders => Set<Order>(); // đại diện cho 1 table trong database
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();
    public DbSet<Product> Prodcuts => Set<Product>();
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        // quét toàn bộ các Configuration Entity (constrain, relationship, key) để đăng kí
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }
}
