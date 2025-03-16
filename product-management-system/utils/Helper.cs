using System.Globalization;

namespace Utils {

    public enum Context {
        DELETING,
        VIEWING,
        UPDATING
    }
    
    public class Helper
    {
        public static void DisplayProducts(List<Product> products) {
            Console.Write($"{"ID".PadRight(6)}");
            Console.Write($"{"Product".PadRight(28)}");
            Console.Write($"{"Price".PadLeft(10)}");
            Console.Write($"{"Created At".PadLeft(26)}\n");

            products.ForEach(product => {

                Console.WriteLine($"{product.Id.ToString().PadRight(5)} {product.Name.PadRight(25)} - {product.Price.ToString("C", CultureInfo.GetCultureInfo("en-PH")).PadLeft(10)} {product.CreatedAt.ToString().PadLeft(25)}");
            });
        }

       public static int CalculateTotalPageSize(int totalProducts, int pageSize) {

            if (totalProducts == pageSize) return 1;
            
            return (totalProducts / pageSize) + 1;
       }

       
       public static bool IsAllowedRole(string[] allowedRoles, string role) {

            return allowedRoles.Contains(role);
        }

        public static string ReadPassword() {

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

        public static void DisplayLoadingIndicator() {
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

        public static void DisplayDashboard(string role) {

            if (role.ToLower() == "admin")
                Console.WriteLine("\n======== PMS Dashboard ========\n1.) New Product\n2.) View Products\n3.) Update Product\n4.) Remove Product\n5.) Search Product\n6.) New User\n7.) View Users\n8.) Logout\n");

            else 
                Console.WriteLine("\n======== PMS Dashboard ========\n1.) View Products\n2. Search Product\n3.) Logout\n");
        }
    }
}