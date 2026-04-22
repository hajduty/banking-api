using Budget.Application.DTOs;
using Budget.Domain.Enums;

using MediatR;

namespace Budget.Application.Accounts.Commands.DepositMoney;

public record DepositMoneyCommand(int AccountId, decimal Amount, CurrencyType Currency) : IRequest<AccountDto>;