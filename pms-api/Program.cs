using Microsoft.EntityFrameworkCore;
using Route;

var AllowedOrigins = "allowed_origins";

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ProductDb>(opt => opt.UseNpgsql("Host=localhost;Database=product_management_system;Username=postgres;Password=''"));
builder.Services.AddCors(options => {
    options.AddPolicy(name: AllowedOrigins, policy => {
        policy.WithOrigins("http://localhost:3000");
    });
});
var app = builder.Build();

var BASE_URL = "/api/v1/products";

app.UseCors(AllowedOrigins);

RouteGroupBuilder products = app.MapGroup(BASE_URL);
ProductRoute.Map(app);

app.Run();
