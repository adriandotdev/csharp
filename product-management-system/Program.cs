using Utils;

ProductManagementContext context = new();
ProductRepository productRepository = new();
UserRepository userRepository = new();

// Use Cases
CreateProductUseCase createProduct = new (productRepository, context);
ShowproductsUseCase showproducts = new (productRepository, context);
UpdateProductUseCase updateProduct = new(productRepository, showproducts, context);
DeleteProductUseCase deleeteProduct = new(productRepository, showproducts);
SearchProductUseCase searchProduct = new(productRepository);
CreateUserUseCase createUser = new(userRepository);
GetUsersUseCase getUsers = new(userRepository);
LoginUseCase login = new(userRepository);
LogoutUseCase logout = new();

Console.Clear();

string title = "================ Welcome to PMS ================";

User? loggedInUser = null;

while (true) {
    login.Run(ref loggedInUser!, title);
    Helper.DisplayLoadingIndicator();

    do {
       
        Console.Clear();

        Helper.DisplayDashboard(loggedInUser.Role);

        Console.Write("Choose: ");
        ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);

        Dictionary<ConsoleKey, Action> dashboardActions = GetDashboardActions(ref loggedInUser);

        if (dashboardActions.TryGetValue(keyInfo.Key, out Action? action)) {
            action.Invoke();
        }
    }
    while (loggedInUser != null);

    Console.Clear();
    Console.WriteLine("Thank you. You have been successfully logged out!");
    Thread.Sleep(1000);
    Console.Clear();
}

Dictionary<ConsoleKey, Action> GetDashboardActions(ref User loggedInUser) {

    var actions = new Dictionary<ConsoleKey, Action>();

    if (loggedInUser.Role == "admin") {

        actions[ConsoleKey.D1] = () => {
            Console.Clear();
            createProduct.Run();
        };
        actions[ConsoleKey.D2] = () => {
            Console.Clear();
            showproducts.Run(Context.VIEWING);
        };
        actions[ConsoleKey.D3] = updateProduct.Run;
        actions[ConsoleKey.D4] = deleeteProduct.Run;
        actions[ConsoleKey.D5] = searchProduct.Run;
        actions[ConsoleKey.D6] = createUser.Run;
        actions[ConsoleKey.D7] = () => {Console.Clear(); getUsers.Run(Context.VIEWING);};
        actions[ConsoleKey.D8] = PerformLogout;
    }
    else if (loggedInUser.Role == "user") {
        actions[ConsoleKey.D1] = () => {
            Console.Clear();
            showproducts.Run(Context.VIEWING);
        };
        actions[ConsoleKey.D2] = searchProduct.Run;
        actions[ConsoleKey.D3] = PerformLogout;
    }

    return actions;
}

void PerformLogout() {
    logout.Run(ref loggedInUser);
}