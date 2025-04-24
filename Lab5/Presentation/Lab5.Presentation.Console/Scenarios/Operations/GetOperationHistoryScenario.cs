using Lab5.Application.Contracts.Accounts;
using Lab5.Application.Contracts.Users;
using Lab5.Application.Models.Operations;
using Spectre.Console;
using System.Globalization;
using System.Text;

namespace Lab5.Presentation.Console.Scenarios.Operations;

public class GetOperationHistoryScenario(IUserService userService) : IScenario
{
    public string Name => "Get operations history";

    public void Run()
    {
        OperationHistoryResult result = userService.GetHistory();

        string message;
        switch (result)
        {
            case OperationHistoryResult.Success success:
                var builder = new StringBuilder();

                builder.AppendLine("Operation history:");

                foreach (Operation operation in success.Operations)
                {
                    builder.AppendLine(
                        CultureInfo.InvariantCulture,
                        $"Username: {operation.UserName}");

                    switch (operation.OperationType)
                    {
                        case OperationType.Withdraw:
                            builder.Append("Withdraw ");
                            break;
                        case OperationType.Replenish:
                            builder.Append("Replenish ");
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                    builder.AppendLine(
                        CultureInfo.InvariantCulture,
                        $"Amount: {operation.Amount}");
                }

                message = builder.ToString();

                break;
            case OperationHistoryResult.Failure:
                message = "Failed to get operations history";
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(result));
        }

        AnsiConsole.WriteLine(message);
        AnsiConsole.Ask<string>("Press q to continue...");
    }
}