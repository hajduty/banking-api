namespace Budget.Domain.Events;

public record AccountOpened(int AccountId, string AccountName, DateTime CreatedAt) : IDomainEvent;