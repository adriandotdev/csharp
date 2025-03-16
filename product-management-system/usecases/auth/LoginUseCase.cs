using Utils;

public class LoginUseCase(IUserRepository repository) {

    public User Run(ref User loggedInUser, string title) {

        while (loggedInUser == null) {

            Console.WriteLine(title);
            Console.Write("\nUsername: ");
            string? username = Console.ReadLine();

            Console.Write("Password: ");
            string? password = Helper.ReadPassword();

            var user = repository.GetUserByUsername(username);

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

        return loggedInUser;
    }
}