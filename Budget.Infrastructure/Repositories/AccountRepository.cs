using Budget.Domain.Entities;
using Budget.Domain.Interfaces;
using Budget.Infrastructure.Data;

namespace Budget.Infrastructure.Repositories;

public class AccountRepository(AppDbContext dbContext) : IAccountRepository
{
    public async Task AddAsync(Account account)
    {
        await dbContext.AddAsync(account);
    }

    public Task<Account> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Account account)
    {
        throw new NotImplementedException();
    }
}
