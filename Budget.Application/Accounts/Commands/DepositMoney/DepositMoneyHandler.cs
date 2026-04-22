using Budget.Application.DTOs;
using Budget.Application.Exceptions;
using Budget.Domain.Interfaces;
using Budget.Domain.ValueObject;
using MediatR;

namespace Budget.Application.Accounts.Commands.DepositMoney;

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

        foreach (var domainEvent in account.DomainEvents)
            await publisher.Publish(domainEvent, cancellationToken);

        account.ClearDomainEvents();

        return AccountDto.FromAccount(account);
    }
}