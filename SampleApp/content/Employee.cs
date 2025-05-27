// Base or Parent class
class Employee {
    string name;
    double salary;

    public Employee(String name, double salary) {
        this.name = name;
        this.salary = salary;
    }

    public void Work() {
        Console.WriteLine($"{this.name} is working");
    }

    public string Name { get; }
    public string Salary { get; }
}

// Child class
class Developer : Employee
{
    private string favoriteProgrammingLanguage;

    public Developer(string name, double salary, string favoriteProgrammingLanguage) : base(name, salary)
    {
        this.favoriteProgrammingLanguage = favoriteProgrammingLanguage;
    }

    public void Code() {
        // You can also call the method of parent class in child class.
        this.Work(); 
        Console.Write($"{this.Name} is coding, and his/her favorite programming language is {this.favoriteProgrammingLanguage}");
    }
}