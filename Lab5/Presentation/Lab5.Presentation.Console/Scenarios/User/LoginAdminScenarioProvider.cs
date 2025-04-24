using Lab5.Application.Contracts.Admin;
using Lab5.Application.Contracts.Users;
using System.Diagnostics.CodeAnalysis;

namespace Lab5.Presentation.Console.Scenarios.User;

public class LoginAdminScenarioProvider(
    IAdminService adminService,
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

        scenario = new LoginAdminScenario(adminService);
        return true;
    }
}