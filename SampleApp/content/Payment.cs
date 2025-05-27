class Payment {

    public virtual void Pay(double amount) {
        // Pay
    }
}

class GCash : Payment {
    public override void Pay(double amount)
    {
       Console.WriteLine($"Paid ${amount} using GCash");
    }
}

class Maya : Payment {
    public override void Pay(double amount)
    {
       Console.WriteLine($"Paid ${amount} using Maya");
    }
}

// In your Program.cs
Payment payment;

payment = new Maya();
payment.Pay(100.0);  // Uses Maya

payment = new GCash();
payment.Pay(200.0);  // Uses GCash