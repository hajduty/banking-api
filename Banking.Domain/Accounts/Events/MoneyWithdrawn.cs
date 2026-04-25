using Banking.Domain.Shared;

namespace Banking.Domain.Accounts.Events;

public record MoneyWithdrawn(int AccountId, Money Amount, DateTime OccurredAt) : IDomainEvent;