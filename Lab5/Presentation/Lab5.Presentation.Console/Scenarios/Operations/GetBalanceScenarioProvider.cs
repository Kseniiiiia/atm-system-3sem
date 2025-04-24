using Lab5.Application.Contracts.Users;
using Lab5.Application.Models.Users;
using System.Diagnostics.CodeAnalysis;

namespace Lab5.Presentation.Console.Scenarios.Operations;

public class GetBalanceScenarioProvider : IScenarioProvider
{
    private readonly IUserService _userService;
    private readonly ICurrentUserService _currentUserService;

    public GetBalanceScenarioProvider(
        IUserService userService, ICurrentUserService currentUser)
    {
        _userService = userService;
        _currentUserService = currentUser;
    }

    public bool TryGetScenario(
        [NotNullWhen(true)] out IScenario? scenario)
    {
        if (_currentUserService.User is null || _currentUserService.User.Role is not UserRole.Customer)
        {
            scenario = null;
            return false;
        }

        scenario = new GetBalanceScenario(_userService);
        return true;
    }
}