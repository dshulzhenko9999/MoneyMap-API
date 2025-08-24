namespace MoneyMap.API.Data.Models;

public class Account
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public decimal Balance { get; set; }
    public AccountType Type { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public Guid UserId { get; set; }

    public virtual User? User { get; set; }
    public virtual ICollection<Transaction> Transactions { get; set; } = [];
}

public enum AccountType
{
    Checking,
    Savings,
    CreditCard,
    Investment,
    Cash,
    Other
}
