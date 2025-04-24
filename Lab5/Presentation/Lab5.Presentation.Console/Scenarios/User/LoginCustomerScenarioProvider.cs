using Lab5.Application.Contracts.Users;
using System.Diagnostics.CodeAnalysis;

namespace Lab5.Presentation.Console.Scenarios.User;

public class LoginCustomerScenarioProvider(
    IUserService userService,
    ICurrentUserService currentUser)
    : IScenarioProvider
{
    public bool TryGetScenario(
        [NotNullWhen(true)] out IScenario? scenario)
    {
        if (currentUser.User is not null)
        {
            scenario = null;
            return false;
        }

        scenario = new LoginCustomerScenario(userService);
        return true;
    }
}