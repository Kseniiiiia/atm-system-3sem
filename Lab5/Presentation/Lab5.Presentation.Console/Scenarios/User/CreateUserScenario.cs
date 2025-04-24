using Lab5.Application.Contracts.Users;
using Spectre.Console;

namespace Lab5.Presentation.Console.Scenarios.User;

public class CreateUserScenario(IUserService userService) : IScenario
{
    public string Name => "Create user";

    public void Run()
    {
        string username;
        do
        {
            username = AnsiConsole.Ask<string>("Enter username:");
        }
        while (string.IsNullOrWhiteSpace(username));

        string password;
        do
        {
            password = AnsiConsole.Ask<string>("Enter password:");
        }
        while (string.IsNullOrWhiteSpace(password));

        CreateResult result1 = userService.CreateUser(username, password);

        string message1 = result1 switch
        {
            CreateResult.Success => "User successfully created",
            CreateResult.Failure => "Failed to create user",
            _ => throw new ArgumentOutOfRangeException(nameof(result1)),
        };

        CreateResult result = userService.CreateAccount(username);

        string message = result switch
        {
            CreateResult.Success => "Account successfully created",
            CreateResult.Failure => "Failed to create account",
            _ => throw new ArgumentOutOfRangeException(nameof(result)),
        };

        AnsiConsole.WriteLine(message1);
        AnsiConsole.WriteLine(message);
        AnsiConsole.Ask<string>("Press q to continue...");
    }
}