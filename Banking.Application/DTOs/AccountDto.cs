using Banking.Domain.Entities;
using Banking.Domain.ValueObject;

namespace Banking.Application.DTOs;

// DTOs should not leak domain types into application output, they should be flattened, init instead of set, records should be immutable
public record AccountDto
{
    public int AccountId { get; init; }
    public string Name { get; init; }
    public decimal Balance { get; init; }
    public string Currency { get; init; }
    public int TransactionCount { get; init; }

    public static AccountDto FromAccount(Account account) => new()
    {
        AccountId = account.Id,
        Name = account.Name,
        Balance = account.Balance.Amount,
        Currency = account.Balance.Currency.ToString(),
        TransactionCount = account.Transactions.Count
    };
}