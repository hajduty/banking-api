using Budget.Domain.ValueObject;

namespace Budget.Domain.Events;

public record MoneyWithdrawn(int AccountId, Money Amount, DateTime OccurredAt) : IDomainEvent;