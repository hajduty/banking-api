using Banking.Application.DTOs;
using Banking.Domain.Shared;
using MediatR;

namespace Banking.Application.Accounts.Commands.WithdrawMoney;

public record WithdrawMoneyCommand(int AccountId, decimal Amount, CurrencyType Currency) : IRequest<AccountDto>;