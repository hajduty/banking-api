using Banking.Domain.Entities;
using Banking.Domain.Enums;
using Banking.Domain.Events;
using Banking.Domain.ValueObject;

namespace Banking.Tests.Domain;

public class AccountTests
{
    public class Creation
    {

        [Fact]
        public void AccountCreationEmptyNameThrows()
        {
            Assert.Throws<ArgumentException>(() => new Account("", CurrencyType.USD));
        }

        [Fact]
        public void AccountCreationTest()
        {
            var account = new Account("TestAccount", CurrencyType.USD);

            Assert.NotNull(account);
        }
    }

    public class Deposit
    {

        [Fact]
        public void AccountDepositTest()
        {
            var account = new Account("TestAccount", CurrencyType.USD);

            Money money = new Money(20, CurrencyType.USD);

            account.Deposit(money);

            Assert.Equal(money, account.Balance);
        }

        [Fact]
        public void DepositCurrencyMismatchThrows()
        {
            var account = new Account("TestAccount", CurrencyType.USD);

            Money money = new Money(20, CurrencyType.SEK);

            Assert.Throws<ArgumentException>(() => account.Deposit(money));
        }

        [Fact]
        public void DepositNoAmountThrows()
        {
            var account = new Account("TestAccount", CurrencyType.USD);

            Assert.Throws<ArgumentException>(() => account.Deposit(new Money(0, CurrencyType.USD)));
        }
    }

    public class Withdraw
    {

        [Fact]
        public void AccountWithdrawTest()
        {
            var account = new Account("TestAccount", CurrencyType.USD);

            Money money = new Money(20, CurrencyType.USD);

            account.Deposit(money);

            account.Withdraw(money);

            Money noMoney = new Money(0, CurrencyType.USD);

            Assert.Equal(noMoney, account.Balance);
        }

        [Fact]
        public void WithdrawNoAmountThrows()
        {
            var account = new Account("TestAccount", CurrencyType.USD);

            account.Deposit(new Money(20, CurrencyType.USD));

            Assert.Throws<ArgumentException>(() => account.Withdraw(new Money(0, CurrencyType.USD)));
        }

        [Fact]
        public void WithdrawCurrencyMismatchThrows()
        {
            var account = new Account("TestAccount", CurrencyType.USD);

            account.Deposit(new Money(20, CurrencyType.USD));

            Assert.Throws<ArgumentException>(() => account.Withdraw(new Money(20, CurrencyType.SEK)));
        }

        [Fact]
        public void WithdrawMoreThanBalanceThrows()
        {
            var account = new Account("TestAccount", CurrencyType.USD);

            account.Deposit(new Money(20, CurrencyType.USD));

            Assert.Throws<InvalidOperationException>(() => account.Withdraw(new Money(200, CurrencyType.USD)));
        }
    }

    public class DomainEvents
    {
        [Fact]
        public void DepositRaisesMoneyDepositedEvent()
        {
            var account = new Account("TestAccount", CurrencyType.USD);

            account.Deposit(new Money(20, CurrencyType.USD));

            var depositEvent = account.DomainEvents
                .OfType<MoneyDeposited>()
                .Single();

            Assert.NotNull(depositEvent);
        }

        [Fact]
        public void WithdrawRaisesMoneyWithdrawnEvent()
        {
            var account = new Account("TestAccount", CurrencyType.USD);

            account.Deposit(new Money(20, CurrencyType.USD));

            account.Withdraw(new Money(20, CurrencyType.USD));

            var depositEvent = account.DomainEvents
                .OfType<MoneyWithdrawn>()
                .Single();

            Assert.NotNull(depositEvent);
        }
    }

    [Fact]
    public void NegativeMoneyThrows()
    {
        Assert.Throws<ArgumentException>(() => new Money(-20, CurrencyType.USD));
    }
}