
public abstract class ATM {
    public abstract void WithdrawMoney(double amount);

    public abstract void DepositMoney(double amount);

    public void CheckBalance() {
        // Login in checking balance
    }
}

public class SecurityBankATM : ATM
{
    public override void DepositMoney(double amount)
    {
        // Logic of depositing money
    }

    public override void WithdrawMoney(double amount)
    {
        // Logic of withdrawing money
    }
}

ATM securityBankATM = new SecurityBankATM();

// Assume there's also an implementation for this.
ATM metrobankATM = new MetrobankATM(); 
