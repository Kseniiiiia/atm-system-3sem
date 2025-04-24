using Lab5.Application.Contracts.Users;
using System.Diagnostics.CodeAnalysis;

namespace Lab5.Presentation.Console.Scenarios.User;

public class CreateUserScenarioProvider(
    IUserService userService,
    ICurrentUserService currentUserService)
    : IScenarioProvider
{
    public bool TryGetScenario([NotNullWhen(true)] out IScenario? scenario)
    {
        if (currentUserService.User is not null)
        {
            scenario = null;
            return false;
        }

        scenario = new CreateUserScenario(userService);
        return true;
    }
}