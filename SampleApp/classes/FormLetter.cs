public class FormLetter {

    public FormLetter() {

        string customerName = "Ms. Barros";

        string currentProduct = "Magic Yield";
        int currentShares = 2975000;
        decimal currentReturn = 0.1275m;
        decimal currentProfit = 55000000.0m;

        string newProduct = "Glorious Future";
        decimal newReturn = 0.13125m;
        decimal newProfit = 63000000.0m;

        string body = $"\nDear {customerName}\nAs a customer of our {currentProduct} offering we are excited to tell you about a new financial product that would dramatically increase your return.\n\nCurrently, you own {currentShares:N0} shares at a return of {currentReturn:P2}.\n\nOur new product, {newProduct} offers a return of ${newReturn:P2}.  Given your current volume, your potential profit would be {newProfit:C}.\n";

        Console.WriteLine(body);

        Console.WriteLine("Here's a quick comparison:\n");

        string currentProductReturn = $"{currentReturn:P2}";
        string newProductReturn = $"{newReturn:P2}";

        string product1 = $"{currentProduct.PadRight(20)}{currentProductReturn.PadRight(9)}{currentProfit:C}";
        string product2 = $"{newProduct.PadRight(20)}{newProductReturn.PadRight(9)}{newProfit:C}";

        string comparisonMessage  = product1 + "\n" + product2;

        Console.WriteLine(comparisonMessage);
    }
}