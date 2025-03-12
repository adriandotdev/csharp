using Utils;

using ProductManagementContext context = new ProductManagementContext();

ProductRepository productRepository = new();

Console.Clear();

string title = "================Welcome to PMS================";

Console.WriteLine(title);

User? loggedInUser = null;

while (loggedInUser == null) {

    Console.Write("\nUsername: ");
    string? username = Console.ReadLine();

    Console.Write("Password: ");
    string? password = ReadPassword();

    var user = context.Users.Where(user => user.Username == username).ToArray();

    if (user == null || user.Length == 0) {
        Console.WriteLine("Invalid credentials");
        continue;
    } else if (user[0].Password != password) {
        Console.WriteLine("\nInvalid credentials");
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
    Console.WriteLine("\nDashboard\n1.) New Product\n2.) View Products\n3.) Update Product\n4.) Remove Product\n5.) Logout\n");
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

            ShowProducts(Context.VIEWING);
            break;
        case 3:
            Console.WriteLine("Update");
            break;
        case 4:
            Console.Clear();
            ShowProducts(Context.DELETING);

            Console.Write("\nEnter the ID of the product you want to delete: ");
            int.TryParse(Console.ReadLine(), out int productId);

            
            break;
        case 5:
            response = "exit";
            break;
        default:
            continue;
    }
}
while (!response.Equals("exit"));

Console.Clear();
Console.WriteLine("Successfully logged out!");

string ReadPassword() {

    string password = "";
    ConsoleKeyInfo key;

    do
    {
        key = Console.ReadKey(true); 

        if (key.Key == ConsoleKey.Enter)
            break;

        if (key.Key == ConsoleKey.Backspace)
        {
            if (password.Length > 0)
            {
                password = password[..^1];
                Console.Write("\b \b"); 
            }
        }
        else
        {
            password += key.KeyChar;
            Console.Write("*"); 
        }
    } while (true);

    return password;
}


void ShowProducts(Context ctx) {

    string? viewExit = "";
    int pageNumber = 1;
    int pageSize = 10;

    do {
        Console.WriteLine("\nProducts\n");
        var products = productRepository.GetProducts(pageNumber, pageSize);
    
        Helper.DisplayProducts(products.products);

        Console.WriteLine($"Page {pageNumber} of {Helper.CalculateTotalPageSize(products.totalProducts, pageSize)}");

        if (ctx == Context.DELETING)
            Console.WriteLine("\nPlease view the product you want to delete\nPress > to go to next page\nPress < to go to previous page\nPress 'Q' to enter the product ID");
        else
            Console.WriteLine("\nPress > to go to next page\nPress < to go to previous page\nPress 'Q' to exit");
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
        else if (keyInfo.Key == ConsoleKey.Q) {

            if (ctx != Context.DELETING)
                Console.Clear();
            break;
        }
    }
    while (viewExit != null && !viewExit.Equals("exit"));
}

