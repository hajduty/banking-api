using Banking.Domain.Shared;

namespace Banking.API.DTOs;

public record CreateAccountRequest(string Name, CurrencyType Currency);