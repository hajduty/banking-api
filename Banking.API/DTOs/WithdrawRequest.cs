using Banking.Domain.Enums;

namespace Budget.API.DTOs;

public record WithdrawRequest(decimal Amount, CurrencyType Currency);