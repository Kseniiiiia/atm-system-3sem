using Lab5.Application.Contracts.Users;
using Lab5.Application.Models.Users;
using System.Diagnostics.CodeAnalysis;

namespace Lab5.Presentation.Console.Scenarios.Operations;

public class WithdrawMoneyScenarioProvider : IScenarioProvider
{
    private readonly IUserService _userService;
    private readonly ICurrentUserService _currentUser;

    public WithdrawMoneyScenarioProvider(
        IUserService userService, ICurrentUserService currentUser)
    {
        _userService = userService;
        _currentUser = currentUser;
    }

    public bool TryGetScenario(
        [NotNullWhen(true)] out IScenario? scenario)
    {
        if (_currentUser.User is null || _currentUser.User.Role is not UserRole.Customer)
        {
            scenario = null;
            return false;
        }

        scenario = new WithdrawMoneyScenario(_userService);
        return true;
    }
}