public class DiceMiniGame {

    public DiceMiniGame() {
        Random random = new Random();

        Console.WriteLine("Would you like to play? (Y/N)");
        if (ShouldPlay()) 
        {
            PlayGame(random);
        }
    }

    void PlayGame(Random random) 
    {
        var play = true;

        while (play) 
        {
            var target = random.Next(1, 100);
            var roll = random.Next(1, 100);

            Console.WriteLine($"Roll a number greater than {target} to win!");
            Console.WriteLine($"You rolled a {roll}");
            Console.WriteLine(WinOrLose(target, roll));
            Console.WriteLine("\nPlay again? (Y/N)");

            play = ShouldPlay();
        }
    }

    string WinOrLose(int target, int roll) 
    {
        return roll > target ? "You win!" : "You lose!";
    }

    bool ShouldPlay() 
    {
        var response = Console.ReadLine();

        return response == "y";
    }
}