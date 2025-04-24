using Lab5.Application.Abstractions.Repositories;
using Lab5.Application.Accounts;
using Lab5.Application.Contracts.Accounts;
using Lab5.Application.Contracts.Users;
using Lab5.Application.Models.Accounts;
using Lab5.Application.Models.Users;

namespace Lab5.Application.Users;

public class UserService(
    IUserRepository userRepository,
    CurrentUserManager currentUserManager,
    IAccountRepository accountRepository,
    IOperationRepository operationRepository)
    : IUserService
{
    public LoginResult Login(string username, string password)
    {
        User? user = userRepository.FindAndCheckUser(username, password);
        if (user == null)
        {
            return new LoginResult.NotFound();
        }

        Account? account = accountRepository.FindAccount(user.UserName);
        if (account == null)
        {
            return new LoginResult.NotFound();
        }

        currentUserManager.User = user;
        currentUserManager.CurrentAccountService = new CurrentAccountManager(account, operationRepository, accountRepository);
        return new LoginResult.Success();
    }

    public BalanceResult GetBalance()
    {
        if (currentUserManager.CurrentAccountService == null)
        {
            return new BalanceResult.Failure();
        }

        return currentUserManager.CurrentAccountService.GetBalance();
    }

    public OperationResult WithdrawMoney(decimal amount)
    {
        if (currentUserManager.CurrentAccountService == null)
        {
            return new OperationResult.Fail();
        }

        return currentUserManager.CurrentAccountService.WithdrawMoney(amount);
    }

    public OperationResult ReplenishMoney(decimal amount)
    {
        if (currentUserManager.CurrentAccountService == null)
        {
            return new OperationResult.Fail();
        }

        return currentUserManager.CurrentAccountService.ReplenishMoney(amount);
    }

    public OperationHistoryResult GetHistory()
    {
        if (currentUserManager.CurrentAccountService == null)
        {
            return new OperationHistoryResult.Failure();
        }

        return currentUserManager.CurrentAccountService.GetHistory();
    }

    public CreateResult CreateUser(string username, string password)
    {
        userRepository.AddUser(new User(username, password, UserRole.Customer));
        return new CreateResult.Success();
    }

    public CreateResult CreateAccount(string username)
    {
        Account? account = accountRepository.AddAccount(username);

        if (account is null)
        {
            return new CreateResult.Failure();
        }

        return new CreateResult.Success();
    }
}