public class CreateUserUseCase(IUserRepository repository) {

    public void Run() {
        Console.Clear(); 
        Console.Write("\nPlease provide a name: ");
        string? name = Console.ReadLine();

        string? username = "";
        while(true) {
            Console.Write("\nPlease provide a username: ");
            username = Console.ReadLine();
            
            var user = repository.GetUserByUsername(username);

            if (user != null) {
                Console.WriteLine("\nUsername already exists");
                continue;
            }

            break;
        }
        string? password = "password";

        Console.Write("\nRoles: \n1.) Admin\n2.) User\nEnter the role of the new user: ");
        string? role = "";

        ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);

        if (keyInfo.Key == ConsoleKey.D1)
            role = "admin";
        else if (keyInfo.Key == ConsoleKey.D2)
            role = "user";

        if (name?.Length > 0 && username?.Length > 0 && role.Length > 0) {

            repository.CreateUser(new User() {
                Name = name,
                Username = username,
                Password = password,
                Role = role
            });
            Console.Clear();
            Console.WriteLine("User successfully created!\nThe default password is \"password\"\nPlease change the password immediately. Thank you.");
            Thread.Sleep(3000);
            Console.Clear();
        }
    }
}