using Banking.Domain.Enums;

namespace Banking.API.DTOs;

public record DepositRequest(decimal Amount, CurrencyType Currency);