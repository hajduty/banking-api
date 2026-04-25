using Banking.Domain.Shared;

namespace Banking.Domain.Accounts.Events;

public record AccountOpened(int AccountId, string AccountName, DateTime CreatedAt) : IDomainEvent;