using Banking.Application;
using Banking.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Banking.Infrastructure.Data;

public class AppDbContext : DbContext, IUnitOfWork
{
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Transaction> Transactions { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>().OwnsOne(p => p.Balance);

        modelBuilder.Entity<Transaction>().OwnsOne(t => t.Amount);

        base.OnModelCreating(modelBuilder);
    }
}
