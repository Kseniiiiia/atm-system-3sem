using Lab5.Application.Contracts.Admin;
using Lab5.Application.Contracts.Users;
using Spectre.Console;

namespace Lab5.Presentation.Console.Scenarios.User;

public class LoginAdminScenario(IAdminService adminService) : IScenario
{
    public string Name => "Admin login";

    public void Run()
    {
        string password = AnsiConsole.Prompt(new TextPrompt<string>("Enter admin's password:")
            .Secret());

        LoginResult result = adminService.Login(password);

        string message;
        switch (result)
        {
            case LoginResult.Success:
                message = "Logged in as admin successfully ";
                break;
            case LoginResult.NotFound:
                message = "Failed to login as admin ";
                AnsiConsole.WriteLine(message);
                Environment.Exit(0);
                break;

            default:
                throw new ArgumentOutOfRangeException(nameof(result));
        }

        AnsiConsole.WriteLine(message);
    }
}