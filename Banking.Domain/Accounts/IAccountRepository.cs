namespace Banking.Domain.Accounts;

public interface IAccountRepository 
{
    Task<Account?> GetByIdAsync(int id);
    Task AddAsync(Account account);
    Task UpdateAsync(Account account);
}
