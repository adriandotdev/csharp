// See https://aka.ms/new-console-template for more information

using ProductManagementContext context = new ProductManagementContext();

Console.Clear();

Console.WriteLine("================Welcome to Yan-Yan Store================");

User? loggedInUser = null;

while (loggedInUser == null) {

    Console.Write("\nUsername: ");
    string? username = Console.ReadLine();

    Console.Write("Password: ");
    string? password = Console.ReadLine();

    var user = context.Users.Where(user => user.Username == username).ToArray();

    if (user == null || user.Length == 0) {
        Console.WriteLine("Invalid credentials");
    } else if (user[0].Password != password) {
        Console.WriteLine("Invalid credentials");
    }

    loggedInUser = user[0];

    
    Console.WriteLine("\nSuccessfully logged in!");
}

