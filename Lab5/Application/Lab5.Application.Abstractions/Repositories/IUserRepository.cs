using Lab5.Application.Models.Users;

namespace Lab5.Application.Abstractions.Repositories;

public interface IUserRepository
{
    User? FindAndCheckUser(string username, string password);

    User? CheckAdminByPassword(string password);

    void AddUser(User user);
}