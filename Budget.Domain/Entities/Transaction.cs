using Budget.Domain.ValueObject;

namespace Budget.Domain.Entities;

public class Transaction
{
    public int Id { get; private set; }
    public Money Amount { get; private set; }
    public DateTime Date { get; private set; }
    public string? Description { get; private set; }
    public int? FromAccountId { get; private set; }
    public int? ToAccountId { get; private set; }

    private Transaction() { }
    public Transaction(Money amount, DateTime date, string? description = null, int? fromAccountId = null, int? toAccountId = null)
    {
        if (amount.Amount <= 0)
            throw new ArgumentException("Transaction amount must be positive.", nameof(amount));

        if (date > DateTime.UtcNow)
            throw new ArgumentException("Date cannot be in the future", nameof(date));

        if (fromAccountId == null && toAccountId == null)
            throw new ArgumentException("A transaction must have at least one account.");

        Amount = amount;
        Date = date;
        Description = description;
        FromAccountId = fromAccountId;
        ToAccountId = toAccountId;
    }
}