using Lab5.Application.Contracts.Accounts;
using Lab5.Application.Contracts.Users;
using Spectre.Console;

namespace Lab5.Presentation.Console.Scenarios.Operations;

public class ReplenishMoneyScenario(IUserService userService) : IScenario
{
    public string Name => "Replenish money";

    public void Run()
    {
        decimal amount = AnsiConsole.Ask<decimal>("Amount to replenish: ");

        OperationResult result = userService.ReplenishMoney(amount);

        string message = result switch
        {
            OperationResult.Success => "Successfully replenish money :)",
            OperationResult.Fail => "Failed to replenish money :(",
            _ => throw new ArgumentOutOfRangeException(nameof(result)),
        };

        AnsiConsole.WriteLine(message);
        AnsiConsole.Ask<string>("Press q to continue...");
    }
}