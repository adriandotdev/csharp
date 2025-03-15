using System.Globalization;

namespace Utils {

    enum Context {
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

            return (totalProducts / pageSize) + 1;
       }
    }
}