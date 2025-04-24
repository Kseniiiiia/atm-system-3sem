namespace Lab5.Application.Contracts.Users;

public abstract record CreateResult
{
    private CreateResult() { }

    public sealed record Success : CreateResult;

    public sealed record Failure : CreateResult;
}