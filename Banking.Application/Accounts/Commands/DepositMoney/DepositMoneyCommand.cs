using Banking.Application.DTOs;
using Banking.Domain.Shared;
using MediatR;

namespace Banking.Application.Accounts.Commands.DepositMoney;

public record DepositMoneyCommand(int AccountId, decimal Amount, CurrencyType Currency) : IRequest<AccountDto>;