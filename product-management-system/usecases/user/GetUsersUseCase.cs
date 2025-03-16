using Utils;

public class GetUsersUseCase(IUserRepository repository) {

    public void Run(Context ctx) {
        string? viewExit = "";
        int pageNumber = 1;
        int pageSize = 10;

        do {
            var response = repository.GetUsers(pageNumber, pageSize);
            
            Console.WriteLine("\nUsers");

            Console.WriteLine($"\n{"ID".PadRight(5)}{"Name".PadRight(20)}{"Username".PadRight(25)}");

            foreach (var user in (IEnumerable<User>)response.users) {
                Console.WriteLine($"{user.Id.ToString().PadRight(5)}{user.Name.PadRight(20)}{user.Username.PadRight(25)}");
            }

            Console.WriteLine($"Page {pageNumber} of {Helper.CalculateTotalPageSize(response.totalUsers, pageSize)}");

             Console.WriteLine("\nPress > to go to next page\nPress < to go to previous page\nPress '(Esc)' to exit");
            ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);

            if (keyInfo.Key == ConsoleKey.RightArrow) {
                Console.Clear();

                if (pageNumber + 1 <= Helper.CalculateTotalPageSize(response.totalUsers, pageSize)) {
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
        while(viewExit != null && !viewExit.Equals("exit"));
     
    }
}