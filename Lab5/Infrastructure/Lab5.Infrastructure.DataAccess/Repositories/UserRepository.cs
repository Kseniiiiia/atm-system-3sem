using Itmo.Dev.Platform.Postgres.Connection;
using Itmo.Dev.Platform.Postgres.Extensions;
using Lab5.Application.Abstractions.Repositories;
using Lab5.Application.Models.Users;
using Npgsql;

namespace Lab5.Infrastructure.DataAccess.Repositories;

public class UserRepository(IPostgresConnectionProvider connectionProvider) : IUserRepository
{
    public User? FindAndCheckUser(string username, string password)
    {
        const string sql =
            """
            select user_name, user_password, user_role
            from users
            where user_name = @username and user_password = @password;
            """;

        NpgsqlConnection connection = connectionProvider
            .GetConnectionAsync(default)
            .Preserve()
            .GetAwaiter()
            .GetResult();

        using var command = new NpgsqlCommand(sql, connection);
        command.AddParameter("username", username);
        command.AddParameter("password", password);

        using NpgsqlDataReader reader = command.ExecuteReader();

        if (reader.Read() is false)
            return null;

        return new User(
            reader.GetString(0),
            reader.GetString(1),
            reader.GetFieldValue<UserRole>(2));
    }

    public User? CheckAdminByPassword(string password)
    {
        const string sql =
            """
            select user_name, user_password, user_role
            from users
            where user_role = 'admin'
            limit 1;
            """;

        NpgsqlConnection connection = connectionProvider
            .GetConnectionAsync(default)
            .Preserve()
            .GetAwaiter()
            .GetResult();

        using var command = new NpgsqlCommand(sql, connection);
        using NpgsqlDataReader reader = command.ExecuteReader();

        if (!reader.Read()) return null;

        string username = reader.GetString(0);
        string userpassword = reader.GetString(1);
        UserRole userrole = reader.GetFieldValue<UserRole>(2);

        var admin = new User(username, userpassword, userrole);
        return string.Equals(password, admin.Password, StringComparison.Ordinal) ? admin : null;
    }

    public void AddUser(User user)
    {
        const string sql =
            """
            insert into users (user_name, user_password, user_role)
            values ( @name, @password, CAST(@role as user_role));
            """;

        NpgsqlConnection connection = connectionProvider
            .GetConnectionAsync(default)
            .Preserve()
            .GetAwaiter()
            .GetResult();

        using var command = new NpgsqlCommand(sql, connection);
        command.AddParameter("name", user.UserName);
        command.AddParameter("password", user.Password);
        command.AddParameter("role", user.Role);

        command.ExecuteNonQuery();
    }
}