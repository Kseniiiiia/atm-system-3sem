using Lab5.Application.Contracts.Users;
using Spectre.Console;

namespace Lab5.Presentation.Console.Scenarios.User;

public class LoginCustomerScenario(IUserService userService) : IScenario
{
    public string Name => "Customer login";

    public void Run()
    {
        string username = AnsiConsole.Ask<string>("Enter username:");
        string password = AnsiConsole.Prompt(new TextPrompt<string>("Enter password:")
            .Secret());

        LoginResult result = userService.Login(username, password);

        string message = result switch
        {
            LoginResult.Success => "Logged in successfully :)",
            LoginResult.NotFound => "User not found :(",
            _ => throw new ArgumentOutOfRangeException(nameof(result)),
        };

        AnsiConsole.WriteLine(message);
        AnsiConsole.Ask<string>("Press q to continue...");
    }
}