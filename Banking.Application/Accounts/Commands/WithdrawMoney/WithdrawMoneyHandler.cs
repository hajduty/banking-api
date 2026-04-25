using Banking.Application.DTOs;
using Banking.Application.Exceptions;
using Banking.Domain.Accounts;
using Banking.Domain.Shared;
using MediatR;

namespace Banking.Application.Accounts.Commands.WithdrawMoney;

public class WithdrawMoneyHandler(
    IAccountRepository accountRepository,
    IUnitOfWork unitOfWork,
    IPublisher publisher)
    : IRequestHandler<WithdrawMoneyCommand, AccountDto>
{
    public async Task<AccountDto> Handle(WithdrawMoneyCommand request, CancellationToken cancellationToken)
    {
        var account = await accountRepository.GetByIdAsync(request.AccountId);

        if (account == null) throw new AccountNotFoundException(request.AccountId);

        var money = new Money(request.Amount, request.Currency);

        account.Withdraw(money);

        await unitOfWork.SaveChangesAsync();

        return AccountDto.FromAccount(account);
    }
}
