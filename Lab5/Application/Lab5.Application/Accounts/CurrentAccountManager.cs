using Lab5.Application.Abstractions.Repositories;
using Lab5.Application.Contracts.Accounts;
using Lab5.Application.Models.Accounts;
using Lab5.Application.Models.Operations;

namespace Lab5.Application.Accounts;

public class CurrentAccountManager(
    Account account,
    IOperationRepository operationRepository,
    IAccountRepository accountRepository)
    : ICurrentAccountService
{
    private Account _account = account;

    public BalanceResult GetBalance()
    {
        return new BalanceResult.Success(accountRepository.GetBalance(_account.UserName));
    }

    public OperationResult WithdrawMoney(decimal amount)
    {
        decimal balance = _account.Balance;
        if (balance < amount)
        {
            return new OperationResult.Fail();
        }

        _account = _account with { Balance = _account.Balance - amount };
        operationRepository.AddOperation(new Operation(_account.UserName, amount, OperationType.Withdraw));
        accountRepository.UpdateBalance(_account.UserName, _account.Balance);
        return new OperationResult.Success();
    }

    public OperationResult ReplenishMoney(decimal amount)
    {
        _account = _account with { Balance = _account.Balance + amount };
        operationRepository.AddOperation(new Operation(_account.UserName, amount, OperationType.Replenish));
        accountRepository.UpdateBalance(_account.UserName, _account.Balance);
        return new OperationResult.Success();
    }

    public OperationHistoryResult GetHistory()
    {
        return new OperationHistoryResult.Success(operationRepository.GetAllAccountOperations(_account.UserName));
    }
}