namespace Lab5.Application.Contracts.Accounts;

public interface ICurrentAccountService
{
    BalanceResult GetBalance();

    OperationResult WithdrawMoney(decimal amount);

    OperationResult ReplenishMoney(decimal amount);

    OperationHistoryResult GetHistory();
}