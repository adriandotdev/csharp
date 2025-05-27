public class BankAccount {

    // Private field (hidden from direct access)
    private double balance; 

    public BankAccount(double initialBalance) {
        this.balance = initialBalance;
    }

    // Public method to modify balance safely
    public void Deposit(double amount) { 
        if (amount > 0) {
            balance += amount;
        }
    }

    // Controlled access to data
    public double GetBalance() { 
        return balance;
    }
}