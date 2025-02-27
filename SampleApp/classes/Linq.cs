using System.Runtime.InteropServices;

public class Linq {

    public Linq() {
        
        List<TechPerson> techPeople = new List<TechPerson> {
            new TechPerson { Name = "John", Age = 25, Job = "Software Engineer", YearsOfExperience = 2 },
            new TechPerson { Name = "Alice", Age = 25, Job = "Software Engineer", YearsOfExperience = 4 },
            new TechPerson { Name = "Bob", Age = 28, Job = "Backend Developer", YearsOfExperience = 1 },
            new TechPerson { Name = "Charlie", Age = 32, Job = "Frontend Developer", YearsOfExperience = 2 },
            new TechPerson { Name = "David", Age = 28, Job = "Backend Developer", YearsOfExperience = 3 },
            new TechPerson { Name = "Eve", Age = 35, Job = "UX Designer", YearsOfExperience = 5 },
            new TechPerson { Name = "Frank", Age = 29, Job = "Product Manager", YearsOfExperience = 3 },
            new TechPerson { Name = "Grace", Age = 31, Job = "UI/UX Designer", YearsOfExperience = 4 },
            new TechPerson { Name = "Harry", Age = 26, Job = "QA Engineer", YearsOfExperience = 2 },
            new TechPerson { Name = "Ivy", Age = 30, Job = "Frontend Developer", YearsOfExperience = 4 },
            new TechPerson { Name = "Jack", Age = 27, Job = "QA Engineer", YearsOfExperience = 3 },
            new TechPerson { Name = "Karen", Age = 33, Job = "Product Manager", YearsOfExperience = 4 },
            new TechPerson { Name = "Lisa", Age = 29, Job = "Frontend Developer", YearsOfExperience = 3 },
        };

        // Find a tech person name Alice
        TechPerson? alice = techPeople.Find(person => person.Name == "Alice");
        List<TechPerson> morethanOrEqualTwoYears = techPeople.FindAll(techPerson => techPerson.YearsOfExperience >= 2);
        TechPerson? oldestTechPerson = techPeople.OrderByDescending(techPerson => techPerson.Age).First();

        // Group By
        var ageGroup = techPeople.GroupBy(techPerson => techPerson.Age);
        var professionGroup = techPeople.GroupBy(techPerson => techPerson.Job);
        var yearsOfExperienceGroup = techPeople.GroupBy(techPerson => techPerson.YearsOfExperience);

        // Order By Ascending
        var sortByAge = techPeople.OrderBy(techPerson => techPerson.Age).ToList();
        var sortByAgeDescending = techPeople.OrderByDescending(techPerson => techPerson.Age).ToList();

        Console.WriteLine($"Alice Information: {alice?.Name}, Age: {alice?.Age}, Job: {alice?.Job}");
        Display(morethanOrEqualTwoYears, "Tech People with more than two years of experience");
        Console.WriteLine($"Oldest Tech Person: {oldestTechPerson.Name}, Age: {oldestTechPerson.Age}");

        DisplayGroupBy(ageGroup, "Age");
        DisplayGroupBy(professionGroup, "Job");
        DisplayGroupBy(yearsOfExperienceGroup, "Years of Experience");
        Display(sortByAge, "Tech People ordered by age (asc)");
        Display(sortByAgeDescending, "Tech People ordered by age (descending)");
    }

    static void DisplayGroupBy<TKey>(IEnumerable<IGrouping<TKey, TechPerson>>? groups, string groupName) {

        if (groups == null)
        {
            Console.WriteLine("No groups found.");
            return;
        }

        Console.WriteLine($"\n{groupName} Groups:");

        foreach (var group in groups)
        {
            Console.WriteLine($"\n{groupName}: {group.Key}, Count: {group.Count()}");
            foreach (var person in group)
            {
                Console.WriteLine($"- {person.Name}, Age: {person.Age}, Job: {person.Job}");
            }
         
        }
    }

    static void Display(List<TechPerson> techPeople, string title) {

        Console.WriteLine($"\n{title}");
        foreach (var person in techPeople)
        {
            Console.WriteLine($"- {person.Name}, Age: {person.Age}, Job: {person.Job}");
        }
    }
}


public class TechPerson {

    public required string Name { get; set; }
    public int Age { get; set; }

    public required string Job { get; set;}

    public required decimal YearsOfExperience { get; set; }

}