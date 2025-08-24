using static MoneyMap.API.Data.Models.Enums.Enums;

namespace MoneyMap.API.Data.Models;

public class Transaction
{
    public Guid Id { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; } = DateTime.UtcNow;
    public required string Description { get; set; }
    public FinancialOperationType Type { get; set; }
    public bool IsRecurring { get; set; } = false;
    public Guid AccountId { get; set; }
    public Guid CategoryId { get; set; }

    public virtual Category? Category { get; set; }
    public virtual Account? Account { get; set; }
}


