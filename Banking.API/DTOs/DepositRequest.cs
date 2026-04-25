using Banking.Domain.Shared;

namespace Banking.API.DTOs;

public record DepositRequest(decimal Amount, CurrencyType Currency);