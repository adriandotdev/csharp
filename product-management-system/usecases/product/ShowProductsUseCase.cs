using Microsoft.EntityFrameworkCore;
using Utils;

public class ShowproductsUseCase {

    private IProductRepository repository;
    private DbContext context;

    public ShowproductsUseCase(IProductRepository repository, DbContext context) {

        this.repository = repository;
        this.context = context;
    }

    public void Run(Context ctx) {
        string? viewExit = "";
        int pageNumber = 1;
        int pageSize = 10;

        do {
            Console.WriteLine("\nProducts\n");
            var products = repository.GetProducts(pageNumber, pageSize);
        
            Helper.DisplayProducts(products.products);

            Console.WriteLine($"Page {pageNumber} of {Helper.CalculateTotalPageSize(products.totalProducts, pageSize)}");

            if (ctx == Context.DELETING)
                Console.WriteLine($"\nPlease view the product you want to delete\nPress > to go to next page\nPress < to go to previous page\nPress '(Esc)' to enter the product ID");
            else if (ctx == Context.UPDATING) 
                Console.WriteLine($"\nPlease view the product you want to update\nPress > to go to next page\nPress < to go to previous page\nPress '(Esc)' to enter the product ID");
            else
                Console.WriteLine("\nPress > to go to next page\nPress < to go to previous page\nPress '(Esc)' to exit");
            ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);

            if (keyInfo.Key == ConsoleKey.RightArrow) {
                Console.Clear();

                if (pageNumber + 1 <= Helper.CalculateTotalPageSize(products.totalProducts, pageSize)) {
                    pageNumber++;
                }
            }
            else if(keyInfo.Key == ConsoleKey.LeftArrow) {
                Console.Clear();
                if (pageNumber - 1 >= 1) {
                    pageNumber--;
                }
            }
            else if (keyInfo.Key == ConsoleKey.Escape) {

                if (ctx == Context.VIEWING)
                    Console.Clear();
                break;
            }
            else {
                Console.Clear();
            }
     }
     while (viewExit != null && !viewExit.Equals("exit"));
    }
}