using Budget.Domain.Enums;
using Budget.Domain.ValueObject;
using Budget.Domain.Events;

namespace Budget.Domain.Entities;

public class Account : AggregateRoot
{
    public int Id { get; private set; }
    public Money Balance { get; private set; }
    public string Name { get; private set; }

    private readonly List<Transaction> _transactions = new();
    public IReadOnlyCollection<Transaction> Transactions => _transactions;

    private Account() { } // For EF Core magic, something something backdoor reflection idk
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
        RaiseDomainEvent(new MoneyDeposited(Id, amount, DateTime.UtcNow));
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
        RaiseDomainEvent(new MoneyWithdrawn(Id, amount, DateTime.UtcNow));
    }
}
