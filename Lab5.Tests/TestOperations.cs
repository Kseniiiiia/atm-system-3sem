using Lab5.Application.Abstractions.Repositories;
using Lab5.Application.Accounts;
using Lab5.Application.Contracts.Accounts;
using Lab5.Application.Models.Accounts;
using NSubstitute;
using Xunit;

namespace Lab5.Tests;

public class TestOperations
{
    [Fact]
    public void WithdrawMoney_ShouldWithdraw_WhenBalanceEnough()
    {
        // Arrange
        decimal amount = new(7);
        var account = new Account("username", 10);

        IAccountRepository accountRepository = Substitute.For<IAccountRepository>();
        accountRepository.GetBalance(Arg.Any<string>()).Returns(10);

        IOperationRepository operationRepository = Substitute.For<IOperationRepository>();

        var accountManager = new CurrentAccountManager(
            account,
            operationRepository,
            accountRepository);

        // Act
        OperationResult result = accountManager.WithdrawMoney(amount);

        // Assert
        Assert.True(result is OperationResult.Success);
    }

    [Fact]
    public void WithdrawMoney_ShouldNotWithdraw_WhenBalanceIsNotEnough()
    {
        // Arrange
        decimal amount = new(7);
        var account = new Account("username", 0);

        IAccountRepository accountRepository = Substitute.For<IAccountRepository>();
        accountRepository.GetBalance(Arg.Any<string>()).Returns(0);

        IOperationRepository operationRepository = Substitute.For<IOperationRepository>();

        var accountManager = new CurrentAccountManager(
            account,
            operationRepository,
            accountRepository);

        // Act
        OperationResult result = accountManager.WithdrawMoney(amount);

        // Assert
        Assert.True(result is OperationResult.Fail);
    }

    [Fact]
    public void ReplenishMoney_ShouldReplenish_ShouldReturnTrue()
    {
        // Arrange
        decimal amount = new(7);
        var account = new Account("username", 0);

        IAccountRepository accountRepository = Substitute.For<IAccountRepository>();
        accountRepository.GetBalance(Arg.Any<string>()).Returns(0);

        IOperationRepository operationRepository = Substitute.For<IOperationRepository>();

        var accountManager = new CurrentAccountManager(
            account,
            operationRepository,
            accountRepository);

        // Act
        OperationResult result = accountManager.ReplenishMoney(amount);

        // Assert
        Assert.True(result is OperationResult.Success);
    }
}