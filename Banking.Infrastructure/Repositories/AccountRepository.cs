using Banking.Domain.Entities;
using Banking.Domain.Interfaces;
using Banking.Infrastructure.Data;

namespace Banking.Infrastructure.Repositories;

public class AccountRepository(AppDbContext dbContext) : IAccountRepository
{
    public async Task AddAsync(Account account)
    {
        await dbContext.Accounts.AddAsync(account);
    }

    public async Task<Account?> GetByIdAsync(int id)
    {
        return await dbContext.Accounts.FindAsync(id);
    }

    public Task UpdateAsync(Account account)
    {
        dbContext.Accounts.Update(account);
        return Task.CompletedTask;
    }
}
