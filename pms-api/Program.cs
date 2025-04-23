using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Route;


var AllowedOrigins = "allowed_origins";

var builder = WebApplication.CreateBuilder(args);

var dbConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Services
builder.Services
    .AddDbContext<ProductDb>(
        opt => opt.UseNpgsql(dbConnectionString
    ));

builder.Services
    .AddCors(
        options => {
            options.AddPolicy(name: AllowedOrigins, policy => {
            policy.WithOrigins("http://localhost:3000").WithMethods("POST", "GET", "PUT", "DELETE", "OPTIONS").AllowAnyHeader();
        });
    });

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer()
    .AddJwtBearer("LocalAuthIssuer", options =>
    {
        options.Authority = builder.Configuration
        ["Authentication:Schemes:LocalAuthIssuer:ValidIssuer"];
        options.Audience = builder.Configuration["Authentication:Schemes:LocalAuthIssuer:ValidAudience"];
    });

builder.Services.AddAuthorization();

builder.Services
    .AddAuthorizationBuilder()
    .AddPolicy("admin_auth", 
        policy => policy
            .RequireRole("admin")
            .RequireClaim("scope", "admin_scope")
    )
    .AddPolicy("user_auth", 
        policy => policy
            .RequireRole("user")
            .RequireClaim("scope", "user_scope")
    )
    .AddPolicy("admin_or_user_auth",
        policy => policy
            .RequireRole("user", "admin")
            .RequireClaim("scope", "admin_scope", "user_scope")
        );

var app = builder.Build();

app.UseCors(AllowedOrigins);
app.UseAuthentication();
app.UseAuthorization();

// Routes
ProductRoute.Map(app);
CategoryRoute.Map(app);
DashboardRoute.Map(app);

app.Run();
