using Banking.Domain.Shared;

namespace Banking.Domain.Accounts.Events;

public record MoneyDeposited(int AccountId, Money Amount, DateTime OccurredAt) : IDomainEvent;