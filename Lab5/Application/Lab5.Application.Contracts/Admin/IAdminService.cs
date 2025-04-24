using Lab5.Application.Contracts.Users;

namespace Lab5.Application.Contracts.Admin;

public interface IAdminService
{
    LoginResult Login(string password);
}