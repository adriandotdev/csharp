using Utils;

public class SearchProductUseCase(IProductRepository repository) {

    public void Run() {
        string? viewExit = "";
        string? searchValue = "";
        
        Console.Clear();
        Console.WriteLine("Search: ");
        Helper.DisplayProducts(repository.GetProducts(searchValue));
        Console.WriteLine("\nPress (Esc) to exit");

        do {
        
            if (Console.KeyAvailable) {

                Console.Clear();
                Console.Write("Search: ");

                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Backspace && searchValue.Length > 0) {
                    searchValue = searchValue[..^1]; 
                    Console.Write(searchValue);
                }
                else if (!char.IsControl(key.KeyChar)) {
                    searchValue += key.KeyChar;
                    Console.Write(searchValue);
                }   
                else if (key.Key == ConsoleKey.Escape) {
                    break;
                }
                var filteredProducts  = repository.GetProducts(searchValue);
                Console.WriteLine();
                if (searchValue.Length > 0 && filteredProducts.Count == 0)  
                    Console.Write("\n====== No products found ======");
                else 
                    Helper.DisplayProducts(filteredProducts);
                Console.WriteLine("\n\nPress (Esc) to exit");
            }
        }
        while(viewExit != null && !viewExit.Equals("exit"));
    }
}