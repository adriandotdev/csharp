using Microsoft.EntityFrameworkCore;

public class ProductManagementContext : DbContext {
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(
            "Host=localhost;Database=product_management_system;Username=postgres;Password=''"
        );
    }
}