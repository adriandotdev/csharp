public class MethodsPractice {

    public MethodsPractice() {

       methodMicrosoftChallenge();
    }

    static void namedMethod(string name, int age, string school) {

        Console.WriteLine($"Hello, my name is {name}. I am {age} years old. I attend {school}.");
    }

    static void optionalMethod(string name, int age = 0, string school = "N/A") {
        Console.WriteLine($"Hello, my name is {name}. I am {age} years old. I attend {school}.");
    }

    static void DisplayEmail(string first, string last, string domain = "hayworth.com") {

        Console.WriteLine($"{first.Substring(0, 2).ToLower()}{last.ToLower()}@{domain}");
    }

    static void methodMicrosoftChallenge() {
        string[,] corporate = 
        {
            {"Robert", "Bavin"}, {"Simon", "Bright"},
            {"Kim", "Sinclair"}, {"Aashrita", "Kamath"},
            {"Sarah", "Delucchi"}, {"Sinan", "Ali"}
        };

        string[,] external = 
        {
            {"Vinnie", "Ashton"}, {"Cody", "Dysart"},
            {"Shay", "Lawrence"}, {"Daren", "Valdes"}
        };

        for (int i = 0; i < corporate.GetLength(0); i++) 
        {
            DisplayEmail(first: corporate[i, 0], last: corporate[i, 1], domain: "contoso.com");
        }

        for (int i = 0; i < external.GetLength(0); i++) 
        {
            DisplayEmail(first: external[i, 0], last: external[i, 1]);
        }
    }
}