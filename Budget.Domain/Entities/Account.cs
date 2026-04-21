using Budget.Domain.Enums;
using Budget.Domain.ValueObject;

namespace Budget.Domain.Entities;

public class Account
{
    public int Id { get; private set; }
    public Money Balance { get; private set; }
    public string Name { get; private set; }

    private readonly List<Transaction> _transactions = new();
    public IReadOnlyCollection<Transaction> Transactions => _transactions;

    public Account(string name, CurrencyType currencyType)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Account name cannot be empty.", nameof(name));

        Name = name;
        Balance = new Money(0, currencyType);
    }

    public void Deposit(Money amount)
    {
        if (amount.Amount <= 0)
            throw new ArgumentException("Deposit amount must be positive.", nameof(amount));
        if (amount.Currency != Balance.Currency)
            throw new ArgumentException("Currency mismatch.", nameof(amount));

        var transaction = new Transaction(amount, DateTime.UtcNow, "Deposit", toAccountId: Id);
        _transactions.Add(transaction);

        Balance = new Money(Balance.Amount + amount.Amount, Balance.Currency);
    }

    public void Withdraw(Money amount)
    {
        if (amount.Amount <= 0)
            throw new ArgumentException("Withdrawal amount must be positive.", nameof(amount));
        if (amount.Currency != Balance.Currency)
            throw new ArgumentException("Currency mismatch.", nameof(amount));
        if (amount.Amount > Balance.Amount)
            throw new InvalidOperationException("Insufficient funds.");

        var transaction = new Transaction(amount, DateTime.UtcNow, "Withdrawal", fromAccountId: Id);
        _transactions.Add(transaction);

        Balance = new Money(Balance.Amount - amount.Amount, Balance.Currency);
    }
}
