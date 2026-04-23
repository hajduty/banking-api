using Banking.Domain.Entities;

namespace Banking.Domain.Interfaces;

public interface IAccountRepository 
{
    Task<Account?> GetByIdAsync(int id);
    Task AddAsync(Account account);
    Task UpdateAsync(Account account);
}
