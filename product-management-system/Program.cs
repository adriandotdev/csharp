using System.Globalization;

using ProductManagementContext context = new ProductManagementContext();

ProductRepository productRepository = new();

Console.Clear();

string title = "================Welcome to Yan-Yan Store================";

Console.WriteLine(title);

User? loggedInUser = null;

while (loggedInUser == null) {

    Console.Write("\nUsername: ");
    string? username = Console.ReadLine();

    Console.Write("Password: ");
    string? password = Console.ReadLine();

    var user = context.Users.Where(user => user.Username == username).ToArray();

    if (user == null || user.Length == 0) {
        Console.WriteLine("Invalid credentials");
        continue;
    } else if (user[0].Password != password) {
        Console.WriteLine("Invalid credentials");
        continue;
    }

    loggedInUser = user[0];


    Console.WriteLine("\nSuccessfully logged in!");
}

string response = "";

Thread.Sleep(1000);
Console.Clear();
Console.WriteLine("Please wait...");
Thread.Sleep(1000);
Console.Clear();

do {
    Console.Clear();
    Console.WriteLine("\nDashboard\n1.) New Product\n2.) View Products\n3.) Logout\n");
    Console.Write("Choose: ");
    int.TryParse(Console.ReadLine(), out int choice);

    switch(choice) {

        case 1: 
            Console.Clear();

            List<Category> categories = productRepository.GetCategories();

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
                productRepository.AddProduct(new Product {
                    Name = productName,
                    Price = price,
                    CategoryId = categoryId
                });
                Console.WriteLine($"\n{productName} successfully added!");
                Thread.Sleep(1500);
            }
           
            break;
        case 2:
            Console.Clear();

            string? viewExit = "";
            int pageNumber = 1;
            int pageSize = 10;

            do {
                Console.WriteLine("\nProducts\n");
                var products = productRepository.GetProducts(pageNumber, pageSize);
           
                DisplayProducts(products.products);

                Console.WriteLine($"Page {pageNumber} of {CalculateTotalPageSize(products.totalProducts, pageSize)}");

               Console.WriteLine("\nPress > to go to next page\nPress < to go to previous page\nPress 'Q' to exit");
               ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);

                if (keyInfo.Key == ConsoleKey.RightArrow) {
                    Console.Clear();

                    if (pageNumber + 1 <= CalculateTotalPageSize(products.totalProducts, pageSize)) {
                        pageNumber++;
                    }
                }
                else if(keyInfo.Key == ConsoleKey.LeftArrow) {
                    Console.Clear();
                    if (pageNumber - 1 >= 1) {
                        pageNumber--;
                    }
                }
                else if (keyInfo.Key == ConsoleKey.Q) {
                    Console.Clear();
                    break;
                }
            }
            while (viewExit != null && !viewExit.Equals("exit"));
            break;
        default:
            response = "exit";
            break;
    }
}
while (!response.Equals("exit"));

Console.Clear();
Console.WriteLine("Successfully logged out");

void DisplayProducts(List<Product> products) {
    Console.WriteLine($"{"ID".PadRight(5)} {"Product".PadRight(25)} {"Price"}\n");
    products.ForEach(product => {

        Console.WriteLine($"{product.Id.ToString().PadRight(5)} {product.Name.PadRight(25)} - {product.Price.ToString("C", CultureInfo.GetCultureInfo("en-PH")).PadLeft(0)}");
    });
}

int CalculateTotalPageSize(int totalProducts, int pageSize) {

    return (totalProducts / pageSize) + 1;
}