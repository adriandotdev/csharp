public class GuessingGame {

    public GuessingGame() {

        Console.WriteLine("Guessing Game");

        Console.WriteLine("Guest a number between 1 and 100");
        int randomNumber = GetRandomNumber();

        // Console.WriteLine(randomNumber); // Print this to show the randon number for testing.
        
        int guess = 0;
        int retries = 0;

        while (randomNumber != guess && retries < 3) {

         Console.Write("Enter your guess: ");
         guess = Convert.ToInt32(Console.ReadLine());

         if (guess == randomNumber) {
             Console.WriteLine("Congratulations! You guessed the number");
         } else {
             Console.WriteLine("Try again");
         }

            retries++;
        }

        if (retries == 3) {
            Console.WriteLine("Game Over! You have reached the maximum number of retries!");
        }
    }

    public static int GetRandomNumber() {

        Random random = new Random();
        return random.Next(1, 100);
    }
}