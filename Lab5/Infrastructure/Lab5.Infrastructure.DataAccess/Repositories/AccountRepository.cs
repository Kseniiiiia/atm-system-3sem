using Itmo.Dev.Platform.Postgres.Connection;
using Itmo.Dev.Platform.Postgres.Extensions;
using Lab5.Application.Abstractions.Repositories;
using Lab5.Application.Models.Accounts;
using Npgsql;

namespace Lab5.Infrastructure.DataAccess.Repositories;

public class AccountRepository(IPostgresConnectionProvider connectionProvider) : IAccountRepository
{
    public Account? FindAccount(string username)
    {
        const string sql =
            """
            select u.user_name, a.account_balance
            from accounts a
            natural join public.users u
            where user_name = @username;
            """;

        NpgsqlConnection connection = connectionProvider
            .GetConnectionAsync(default)
            .Preserve()
            .GetAwaiter()
            .GetResult();

        using var command = new NpgsqlCommand(sql, connection);
        command.AddParameter("username", username);

        using NpgsqlDataReader reader = command.ExecuteReader();

        if (reader.Read() is false)
            return null;

        return new Account(
            reader.GetString(0),
            reader.GetDecimal(1));
    }

    public Account? AddAccount(string username)
    {
        const string sql1 =
            """
            insert into accounts (user_id, user_name, account_balance)
            select user_id, user_name, @balance
            from users
            where users.user_name = @username
            limit 1;
            """;
        NpgsqlConnection connection1 = connectionProvider
            .GetConnectionAsync(default)
            .Preserve()
            .GetAwaiter()
            .GetResult();

        using var command1 = new NpgsqlCommand(sql1, connection1);
        command1.AddParameter("username", username);
        command1.AddParameter("balance", 0);

        command1.ExecuteNonQuery();

        return new Account(username, 0);
    }

    public decimal GetBalance(string username)
    {
        const string sql =
            """
            select account_balance
            from accounts
            where user_name = @username;
            """;

        NpgsqlConnection connection = connectionProvider
            .GetConnectionAsync(default)
            .Preserve()
            .GetAwaiter()
            .GetResult();

        using var command = new NpgsqlCommand(sql, connection);
        command.AddParameter("username", username);

        using NpgsqlDataReader reader = command.ExecuteReader();

        if (reader.Read())
        {
            return reader.GetDecimal(0);
        }

        return decimal.Zero;
    }

    public void UpdateBalance(string username, decimal amount)
    {
        const string sql =
            """
            update accounts
            set account_balance = @amount
            where user_name = @username;
            """;

        NpgsqlConnection connection = connectionProvider
            .GetConnectionAsync(default)
            .Preserve()
            .GetAwaiter()
            .GetResult();

        using var command = new NpgsqlCommand(sql, connection);
        command.AddParameter("username", username);
        command.AddParameter("amount", amount);

        command.ExecuteNonQuery();
    }
}