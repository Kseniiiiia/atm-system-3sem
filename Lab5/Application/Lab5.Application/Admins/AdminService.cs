using Lab5.Application.Abstractions.Repositories;
using Lab5.Application.Contracts.Admin;
using Lab5.Application.Contracts.Users;
using Lab5.Application.Models.Users;
using Lab5.Application.Users;

namespace Lab5.Application.Admins;

public class AdminService(
    IUserRepository userRepository,
    CurrentUserManager currentUserManager)
    : IAdminService
{
    public LoginResult Login(string password)
    {
        User? admin = userRepository.CheckAdminByPassword(password);
        if (admin == null)
        {
            return new LoginResult.NotFound();
        }

        currentUserManager.User = admin;
        return new LoginResult.Success();
    }
}