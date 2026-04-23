using Banking.Application.DTOs;
using Banking.Domain.Enums;

using MediatR;

namespace Banking.Application.Accounts.Commands.DepositMoney;

public record DepositMoneyCommand(int AccountId, decimal Amount, CurrencyType Currency) : IRequest<AccountDto>;