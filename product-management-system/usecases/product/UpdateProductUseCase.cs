using Microsoft.EntityFrameworkCore;
using Utils;

public class UpdateProductUseCase {

    private IProductRepository repository;
    private DbContext context;

    private ShowproductsUseCase showProduct;

    public UpdateProductUseCase(IProductRepository repository, ShowproductsUseCase showProduct,  DbContext context) {

        this.repository = repository;
        this.showProduct = showProduct;
        this.context = context;
    }

    public void Run() {
        Console.Clear();

        this.showProduct.Run(Context.UPDATING);

        Console.Write("\nEnter the ID of the product you want to update: ");
        bool isValidProductId = int.TryParse(Console.ReadLine(), out int productToUpdateID);

        if (!isValidProductId) return;

        Console.Write("\nEnter new name: (Leave blank to keep current): ");
        string? newProductName = Console.ReadLine();

        Console.Write("\nEnter new price: (Leave blank to keep current): ");
        int.TryParse(Console.ReadLine(), out int newProductPrice);

        var productToUpdate = this.repository.GetProductById(productToUpdateID);

        productToUpdate.Name = newProductName?.Length > 0 ? newProductName : productToUpdate.Name;

        productToUpdate.Price = newProductPrice > 0 ? newProductPrice : productToUpdate.Price;

        context.SaveChanges();

        Console.WriteLine($"\nProduct {productToUpdate.Name} successfully updated!");
        Thread.Sleep(1500);
    }
}