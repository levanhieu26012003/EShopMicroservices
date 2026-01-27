var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
    config.AddOpenBehavior(typeof(ValidatorBehavior<,>)); // tự động validate
    config.AddOpenBehavior(typeof(LoggingBehavior<,>)); 
}
);

builder.Services.AddMarten(options =>
{
    options.Connection(builder.Configuration.GetConnectionString("Database")!);
    options.Schema.For<ShoppingCart>().Identity(x => x.UserName);
}).UseLightweightSessions();

builder.Services.AddScoped<IBasketRepository, BasketRepository>(); 
builder.Services.Decorate<IBasketRepository, CacheBasketRepository>();// vào CacheBasketRepository trước khi vào các instance implement của IBasketRepository

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
}); // cấu hình redis như một db tạm

builder.Services.AddExceptionHandler<CustomerExceptionHandler>();

builder.Services.AddCarter();

builder.Services.AddHealthChecks().
    AddNpgSql(builder.Configuration.GetConnectionString("Database")!)
    .AddRedis(builder.Configuration.GetConnectionString("Redis")!);
var app = builder.Build();
app.MapCarter();
app.UseExceptionHandler(options =>
{
});
app.UseHealthChecks("/health",
    new HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    }); app.Run();
