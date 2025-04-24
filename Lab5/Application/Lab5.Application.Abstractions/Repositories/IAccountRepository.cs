using Lab5.Application.Models.Accounts;

namespace Lab5.Application.Abstractions.Repositories;

public interface IAccountRepository
{
    Account? FindAccount(string username);

    Account? AddAccount(string username);

    decimal GetBalance(string username);

    void UpdateBalance(string username, decimal amount);
}