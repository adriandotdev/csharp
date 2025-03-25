using Microsoft.EntityFrameworkCore;
using Route;

var AllowedOrigins = "allowed_origins";

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ProductDb>(opt => opt.UseNpgsql("Host=localhost;Database=product_management_system;Username=postgres;Password=''"));
builder.Services.AddCors(options => {
    options.AddPolicy(name: AllowedOrigins, policy => {
        policy.WithOrigins("http://localhost:3000").WithMethods("POST", "GET", "PUT", "DELETE", "OPTIONS").AllowAnyHeader();
    });
});
var app = builder.Build();

app.UseCors(AllowedOrigins);

ProductRoute.Map(app);
CategoryRoute.Map(app);

app.Run();
