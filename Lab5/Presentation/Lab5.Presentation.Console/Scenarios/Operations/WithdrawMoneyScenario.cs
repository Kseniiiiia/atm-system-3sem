using Lab5.Application.Contracts.Accounts;
using Lab5.Application.Contracts.Users;
using Spectre.Console;

namespace Lab5.Presentation.Console.Scenarios.Operations;

public class WithdrawMoneyScenario(IUserService userService) : IScenario
{
    public string Name => "Withdraw money";

    public void Run()
    {
        decimal amount = AnsiConsole.Ask<decimal>("Amount to withdraw:");

        OperationResult result = userService.WithdrawMoney(amount);

        string message = result switch
        {
            OperationResult.Success => "Successfully withdraw money",
            OperationResult.Fail => "Failed to withdraw money",
            _ => throw new ArgumentOutOfRangeException(nameof(result)),
        };

        AnsiConsole.WriteLine(message);
        AnsiConsole.Ask<string>("Press q to continue...");
    }
}