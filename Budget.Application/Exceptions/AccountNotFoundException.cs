namespace Budget.Application.Exceptions;

public class AccountNotFoundException(int accountId)
    : Exception($"Account with id {accountId} was not found.");