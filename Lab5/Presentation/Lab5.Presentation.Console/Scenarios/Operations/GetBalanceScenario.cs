using Lab5.Application.Contracts.Accounts;
using Lab5.Application.Contracts.Users;
using Spectre.Console;

namespace Lab5.Presentation.Console.Scenarios.Operations;

public class GetBalanceScenario(IUserService userService) : IScenario
{
    public string Name => "Get balance";

    public void Run()
    {
        BalanceResult result = userService.GetBalance();

        string message = result switch
        {
            BalanceResult.Success success => $"Balance is {success.Balance}.",
            BalanceResult.Failure => "Failed to get the balance",
            _ => throw new ArgumentOutOfRangeException(nameof(result)),
        };

        AnsiConsole.WriteLine(message);
        AnsiConsole.Ask<string>("Press q to continue...");
    }
}