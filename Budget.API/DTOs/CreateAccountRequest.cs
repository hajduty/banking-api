using Budget.Domain.Enums;

namespace Budget.API.DTOs;

public record CreateAccountRequest(string Name, CurrencyType Currency);