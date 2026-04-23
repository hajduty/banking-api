using Banking.Application.DTOs;
using Banking.Domain.Enums;
using MediatR;

namespace Banking.Application.Accounts.Commands.CreateAccount;

public record CreateAccountCommand(string Name, CurrencyType Currency) : IRequest<AccountDto>;