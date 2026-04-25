using Banking.Domain.Shared;

namespace Budget.API.DTOs;

public record WithdrawRequest(decimal Amount, CurrencyType Currency);