using Budget.Domain.Enums;

namespace Budget.Domain.ValueObject;

public record Money
{
    public decimal Amount { get; init; }
    public CurrencyType Currency { get; init; }

    public Money(decimal amount, CurrencyType currency)
    {
        if (amount < 0)
            throw new ArgumentException("Money cannot be negative.", nameof(amount));

        Amount = amount;
        Currency = currency;
    }
}