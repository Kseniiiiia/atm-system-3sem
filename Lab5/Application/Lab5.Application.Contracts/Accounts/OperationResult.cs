namespace Lab5.Application.Contracts.Accounts;

public abstract record OperationResult
{
    private OperationResult() { }

    public sealed record Success : OperationResult;

    public sealed record Fail : OperationResult;
}