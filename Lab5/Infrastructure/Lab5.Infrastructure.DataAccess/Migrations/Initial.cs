using FluentMigrator;
using Itmo.Dev.Platform.Postgres.Migrations;

namespace Lab5.Infrastructure.DataAccess.Migrations;

[Migration(1, "Initial")]
public class Initial : SqlMigration
{
    protected override string GetUpSql(IServiceProvider serviceProvider) =>
        """
        create table abc
        (
            id bigint primary key generated always as identity
        )
        
        create type user_role as enum
        (
            'admin',
            'customer'
        );

        create type operation_type as enum
        (
            'withdraw',
            'replenish'
        );

        create table users
        (
            user_id bigint primary key generated always as identity,
            user_name text not null,
            user_password text not null,
            user_role user_role not null
        );

        create table accounts
        (
            user_id bigint not null references users(user_id),
            user_name text not null,
            account_balance numeric(100, 2) not null
        );

        create table operations
        (
            user_id bigint not null references users(user_id),
            user_name text not null,
            operation_amount numeric(100, 2) not null,
            operation_type operation_type not null
        );

        """;

    protected override string GetDownSql(IServiceProvider serviceProvider) =>
        """
        drop table accounts;
        drop table operations;
        drop table users;
        drop table abc;

        drop type user_role;
        drop type operation_type;
        
        """;
}