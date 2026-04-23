using Banking.Application.DTOs;
using Banking.Domain.Entities;
using Banking.Domain.Events;
using Banking.Domain.Interfaces;
using MediatR;

namespace Banking.Application.Accounts.Commands.CreateAccount;

public class CreateAccountHandler(IAccountRepository accountRepository, IUnitOfWork unitOfWork, IPublisher publisher) 
    : IRequestHandler<CreateAccountCommand, AccountDto>
{
    public async Task<AccountDto> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
    {
        var account = new Account(request.Name, request.Currency);

        await accountRepository.AddAsync(account);

        await unitOfWork.SaveChangesAsync();

        account.RaiseDomainEvent(new AccountOpened(account.Id, account.Name, DateTime.UtcNow));

        return AccountDto.FromAccount(account);
    }
}