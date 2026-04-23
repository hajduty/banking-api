using Banking.Domain.ValueObject;

namespace Banking.Domain.Events;

public record MoneyDeposited(int AccountId, Money Amount, DateTime OccurredAt) : IDomainEvent;