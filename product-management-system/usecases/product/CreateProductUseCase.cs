using Microsoft.EntityFrameworkCore;

public class CreateProductUseCase {

    private IProductRepository repository;
    private DbContext ctx;

    public CreateProductUseCase(IProductRepository repository, ProductManagementContext ctx) {

        this.repository = repository;
        this.ctx = ctx;
    }

    public void Run() {
        List<Category> categories = repository.GetCategories();

        Console.WriteLine("New Product");

        Console.Write("\nName: ");
        string? productName = Console.ReadLine();

        Console.Write("\nPrice: ");
        bool isValidPrice = decimal.TryParse(Console.ReadLine(), out decimal price);

        Console.WriteLine();
        categories.ForEach(category => {
            Console.WriteLine($"{category.Id}.) {category.Name}");
        });
        Console.Write("Please select a valid category: ");
        bool isValidCategoryId = int.TryParse(Console.ReadLine(), out int categoryId);

        if (productName != null && isValidPrice && isValidCategoryId) {
            repository.AddProduct(new Product {
                Name = productName,
                Price = price,
                CategoryId = categoryId
            });
            Console.WriteLine($"\n{productName} successfully added!");
            Thread.Sleep(1500);
        }
    }
}