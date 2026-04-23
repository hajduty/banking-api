using Budget.Application.DTOs;
using Budget.Domain.Enums;
using MediatR;

namespace Budget.Application.Accounts.Commands.CreateAccount;

public record CreateAccountCommand(string Name, CurrencyType Currency) : IRequest<AccountDto>;