using Banking.Domain.Enums;

namespace Banking.API.DTOs;

public record CreateAccountRequest(string Name, CurrencyType Currency);