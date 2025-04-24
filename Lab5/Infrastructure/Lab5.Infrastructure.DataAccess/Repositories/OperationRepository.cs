using Itmo.Dev.Platform.Postgres.Connection;
using Itmo.Dev.Platform.Postgres.Extensions;
using Lab5.Application.Abstractions.Repositories;
using Lab5.Application.Models.Operations;
using Npgsql;

namespace Lab5.Infrastructure.DataAccess.Repositories;

public class OperationRepository(IPostgresConnectionProvider connectionProvider) : IOperationRepository
{
    public IEnumerable<Operation> GetAllAccountOperations(string username)
    {
        const string sql =
            """
            select user_name, operation_amount, operation_type
            from operations
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

        while (reader.Read() is not false)
        {
            yield return new Operation(
                reader.GetString(0),
                reader.GetDecimal(1),
                reader.GetFieldValue<OperationType>(2));
        }
    }

    public void AddOperation(Operation operation)
    {
        const string sql =
            """
            insert into operations (user_id, user_name, operation_amount, operation_type)
            select user_id, user_name, @amount, CAST(@operation_type as operation_type)
            from users
            where users.user_name = @username
            limit 1;
            """;

        NpgsqlConnection connection = connectionProvider
            .GetConnectionAsync(default)
            .Preserve()
            .GetAwaiter()
            .GetResult();

        using var command = new NpgsqlCommand(sql, connection);
        command.AddParameter("username", operation.UserName);
        command.AddParameter("amount", operation.Amount);
        command.AddParameter("operation_type", operation.OperationType);

        command.ExecuteNonQuery();
    }
}