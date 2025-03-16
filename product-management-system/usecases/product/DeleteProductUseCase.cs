using Utils;

public class DeleteProductUseCase(
    IProductRepository repository, 
    ShowproductsUseCase showProduct) 
{
    public void Run() {
        Console.Clear();
        showProduct.Run(Context.DELETING);

        Console.Write("\nEnter the ID of the product you want to delete (Leave blank if you want to cancel): ");
        int.TryParse(Console.ReadLine(), out int productId);

        var productIsDeleted = repository.DeleteProductById(productId);

        if (productIsDeleted) {
            Console.WriteLine($"\nProduct with ID of {productId} successfully deleted!");
        }
        else {
            Console.WriteLine($"Product not found.");
        }
        Thread.Sleep(1000);
    }
}