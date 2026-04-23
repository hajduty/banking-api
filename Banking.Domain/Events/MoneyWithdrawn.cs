using Banking.Domain.ValueObject;

namespace Banking.Domain.Events;

public record MoneyWithdrawn(int AccountId, Money Amount, DateTime OccurredAt) : IDomainEvent;