using Lab5.Application.Contracts.Accounts;

namespace Lab5.Application.Contracts.Users;

public interface IUserService
{
    LoginResult Login(string username, string password);

    BalanceResult GetBalance();

    OperationResult WithdrawMoney(decimal amount);

    OperationResult ReplenishMoney(decimal amount);

    OperationHistoryResult GetHistory();

    CreateResult CreateUser(string username, string password);

    CreateResult CreateAccount(string username);
}