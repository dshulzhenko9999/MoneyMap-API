namespace MoneyMap.API.Data.Models;

public class User
{
    public Guid Id { get; set; }
    public required string UserName { get; set; }
    public required string Email { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Secure password storage
    public required byte[] PasswordHash { get; set; }
    public required byte[] PasswordSalt { get; set; }

    public virtual ICollection<Account> Accounts { get; set; } = [];
}

