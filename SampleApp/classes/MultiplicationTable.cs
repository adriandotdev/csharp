public class MultiplicationTable {

    public MultiplicationTable() {
        Console.WriteLine("Multiplication Table");

        Console.Write("Enter a number to print multiplication table: ");
        int number = Convert.ToInt32(Console.ReadLine());
        PrintMultiplicationTable(number);
    }

    public void PrintMultiplicationTable(int number) {
        for (int i = 1; i <= 10; i++) {
            Console.WriteLine($"{number} * {i} = {number * i}");
        }
    }
}