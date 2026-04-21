using Budget.Domain.ValueObject;

namespace Budget.Domain.Events;

public record MoneyDeposited(int AccountId, Money Amount, DateTime OccurredAt) : IDomainEvent;