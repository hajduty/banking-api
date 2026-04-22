using Budget.Domain.Enums;

namespace Budget.API.DTOs;

public record DepositRequest(decimal Amount, CurrencyType Currency);