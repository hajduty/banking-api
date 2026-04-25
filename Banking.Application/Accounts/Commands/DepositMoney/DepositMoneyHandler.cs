using Banking.Application.DTOs;
using Banking.Application.Exceptions;
using Banking.Domain.Accounts;
using Banking.Domain.Shared;
using MediatR;

namespace Banking.Application.Accounts.Commands.DepositMoney;

public class DepositMoneyCommandHandler(IAccountRepository accountRepository, IUnitOfWork unitOfWork, IPublisher publisher)
    : IRequestHandler<DepositMoneyCommand, AccountDto>
{
    public async Task<AccountDto> Handle(DepositMoneyCommand request, CancellationToken cancellationToken)
    {
        var account = await accountRepository.GetByIdAsync(request.AccountId);

        if (account == null) throw new AccountNotFoundException(request.AccountId);

        Money money = new Money(request.Amount, request.Currency);

        account.Deposit(money);

        await unitOfWork.SaveChangesAsync();

        return AccountDto.FromAccount(account);
    }
}