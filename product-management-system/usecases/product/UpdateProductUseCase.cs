using Microsoft.EntityFrameworkCore;
using Utils;

public class UpdateProductUseCase(
    IProductRepository repository, 
    ShowproductsUseCase showProduct, 
    DbContext context
    ) 
{

    public void Run() {
        Console.Clear();

        showProduct.Run(Context.UPDATING);

        Console.Write("\nEnter the ID of the product you want to update: ");
        bool isValidProductId = int.TryParse(Console.ReadLine(), out int productToUpdateID);

        if (!isValidProductId) {
            Console.WriteLine("Product not found.");
            Thread.Sleep(1000);
            return;
        };

        Console.Write("\nEnter new name: (Leave blank to keep current): ");
        string? newProductName = Console.ReadLine();

        Console.Write("\nEnter new price: (Leave blank to keep current): ");
        int.TryParse(Console.ReadLine(), out int newProductPrice);

        var productToUpdate = repository.GetProductById(productToUpdateID);

        productToUpdate.Name = newProductName?.Length > 0 ? newProductName : productToUpdate.Name;

        productToUpdate.Price = newProductPrice > 0 ? newProductPrice : productToUpdate.Price;

        context.SaveChanges();

        Console.WriteLine($"\nProduct {productToUpdate.Name} successfully updated!");
        Thread.Sleep(1500);
    }
}