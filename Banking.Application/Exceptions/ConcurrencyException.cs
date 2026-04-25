namespace Banking.Application.Exceptions;

public class ConcurrencyException()
    : Exception($"RowVersion mismatch");