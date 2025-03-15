using Utils;

ProductManagementContext context = new();
ProductRepository productRepository = new();
UserRepository userRepository = new();

Console.Clear();

string title = "================ Welcome to PMS ================";

User? loggedInUser = null;

while (true) {
    Login();
    DisplayLoadingIndicator();

    string response = "";

    do {
        Console.Clear();
        DisplayDashboard(loggedInUser!.Role);


        Console.Write("Choose: ");
        ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);

        
        switch(keyInfo.Key) {

            case ConsoleKey.D1: 
                if (!IsAllowedRole(["admin"], loggedInUser.Role)) break;

                Console.Clear();
                CreateProduct();
                break;
            case ConsoleKey.D2:
                if (!IsAllowedRole(["admin", "user"], loggedInUser.Role)) break;

                Console.Clear();
                ShowProducts(Context.VIEWING);
                break;
            case ConsoleKey.D3:
                if (!IsAllowedRole(["admin"], loggedInUser.Role)) break;

                Console.Clear();

                ShowProducts(Context.UPDATING);

                Console.Write("\nEnter the ID of the product you want to update: ");
                bool isValidProductId = int.TryParse(Console.ReadLine(), out int productToUpdateID);

                if (!isValidProductId) continue;

                Console.Write("\nEnter new name: (Leave blank to keep current): ");
                string? newProductName = Console.ReadLine();

                Console.Write("\nEnter new price: (Leave blank to keep current): ");
                int.TryParse(Console.ReadLine(), out int newProductPrice);

                var productToUpdate = productRepository.GetProductById(productToUpdateID);

                productToUpdate.Name = newProductName?.Length > 0 ? newProductName : productToUpdate.Name;

                productToUpdate.Price = newProductPrice > 0 ? newProductPrice : productToUpdate.Price;

                context.SaveChanges();

                Console.WriteLine($"\nProduct {productToUpdate.Name} successfully updated!");
                Thread.Sleep(1500);
                break;
            case ConsoleKey.D4: 
                if (!IsAllowedRole(["admin"], loggedInUser.Role)) break;

                Console.Clear();
                ShowProducts(Context.DELETING);

                Console.Write("\nEnter the ID of the product you want to delete (Leave blank if you want to cancel): ");
                int.TryParse(Console.ReadLine(), out int productId);

                var productIsDeleted = productRepository.DeleteProductById(productId);

                if (productIsDeleted) {
                    Console.WriteLine($"\nProduct with ID of {productId} successfully deleted!");
                }
                else {
                    Console.WriteLine($"Product with ID of {productId} is not found.");
                }
                Thread.Sleep(1000);
                break;
            case ConsoleKey.D5: // Search
                if (!IsAllowedRole(["admin", "user"], loggedInUser.Role)) break;

                string? viewExit = "";
                string? searchValue = "";
                
                Console.Clear();
                Console.WriteLine("Search: ");
                Helper.DisplayProducts(productRepository.GetProducts(searchValue));
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
                        var filteredProducts  = productRepository.GetProducts(searchValue);
                        Console.WriteLine();
                        if (searchValue.Length > 0 && filteredProducts.Count == 0)  
                            Console.Write("\n====== No products found ======");
                        else 
                            Helper.DisplayProducts(filteredProducts);
                        Console.WriteLine("\n\nPress (Esc) to exit");
                    }
                }
                while(viewExit != null && !viewExit.Equals("exit"));
                break;
            case ConsoleKey.D6:
                loggedInUser = null;
                response = "logout";
                break;
            default:
                continue;
        }
    }
    while (!response.Equals("logout"));

    Console.Clear();
    Console.WriteLine("Successfully logged out!");
    Thread.Sleep(1000);
    Console.Clear();
}


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

void CreateProduct() {
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

void DisplayLoadingIndicator() {
    Thread.Sleep(1000);
    Console.Clear();

    string[] chars = {".", ".", "."};
    string message = "Please wait";

    for (int i = 0; i < 3; i++) {
        message = "Please wait";

        foreach(string s in chars) {
            message +=  s;
            Console.Write($"\r{message.PadRight(Console.WindowWidth)}");
            Thread.Sleep(200);
        }
    }

    Thread.Sleep(1000);
    Console.Clear();
}

void Login() {
    while (loggedInUser == null) {

        Console.WriteLine(title);
        Console.Write("\nUsername: ");
        string? username = Console.ReadLine();

        Console.Write("Password: ");
        string? password = ReadPassword();

        var user = userRepository.GetUserByUsername(username);

        if (user == null) {
        
            Console.WriteLine("\nInvalid credentials");
            Thread.Sleep(1000);
            Console.Clear();
            continue;
        } else if (user.Password != password) {
            Console.WriteLine("\nInvalid credentials");
            Thread.Sleep(1000);
            Console.Clear();
            continue;
        }

        loggedInUser = user;

        Console.WriteLine("\nSuccessfully logged in!");
    }
}

void DisplayDashboard(string role) {

    if (role.ToLower() == "admin")
        Console.WriteLine("\n======== PMS Dashboard ========\n1.) New Product\n2.) View Products\n3.) Update Product\n4.) Remove Product\n5. Search Product\n6.) Logout\n");

    else 
        Console.WriteLine("\n======== PMS Dashboard ========\n2.) View Products\n5. Search Product\n6.) Logout\n");
}

bool IsAllowedRole(string[] allowedRoles, string role) {

    return allowedRoles.Contains(role);
}