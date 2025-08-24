using static MoneyMap.API.Data.Models.Enums.Enums;

namespace MoneyMap.API.Data.Models;

public class Category
{
    public Guid Id { get; set; }
    public string? Description { get; set; }
    public FinancialOperationType Type { get; set; }

    public virtual ICollection<Transaction> Transactions { get; set; } = [];
}
