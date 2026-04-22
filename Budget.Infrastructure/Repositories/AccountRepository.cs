using Budget.Domain.Entities;
using Budget.Domain.Interfaces;
using Budget.Infrastructure.Data;

namespace Budget.Infrastructure.Repositories;

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
