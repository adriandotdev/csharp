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
    Thread.Sleep(2000);
}

string response = "";

Thread.Sleep(1000);
Console.Clear();
Console.WriteLine("Please wait...");
Thread.Sleep(1000);
Console.Clear();
Console.WriteLine(title);

do {

    Console.WriteLine("\n1.) New Product\n2.) View Products\n3.) Exit\n");
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
            do {
                Console.WriteLine("\nProducts\n");
                productRepository.GetProducts().ForEach(product => {

                    Console.WriteLine($"{product.Id}.) {product.Name} - {product.Price}");
                });

                Console.WriteLine("\nPress > to go to next page\nPress < to go to previous page\nPress 'Q' to exit");
                viewExit = Console.ReadLine();

                if (!viewExit.ToLower().Equals("q")) {

                }
                else {
                    viewExit = "exit";
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


