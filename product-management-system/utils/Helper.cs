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
            Console.WriteLine($"{"ID".PadRight(5)} {"Product".PadRight(25)} {"Price"}\n");
            products.ForEach(product => {

                Console.WriteLine($"{product.Id.ToString().PadRight(5)} {product.Name.PadRight(25)} - {product.Price.ToString("C", CultureInfo.GetCultureInfo("en-PH")).PadLeft(0)}");
            });
        }

       public static int CalculateTotalPageSize(int totalProducts, int pageSize) {

            return (totalProducts / pageSize) + 1;
       }
    }
}