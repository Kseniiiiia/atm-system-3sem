using Lab5.Application.Models.Operations;

namespace Lab5.Application.Contracts.Accounts;

public abstract record OperationHistoryResult
{
    private OperationHistoryResult() { }

    public sealed record Success(IEnumerable<Operation> Operations)
        : OperationHistoryResult;

    public sealed record Failure : OperationHistoryResult;
}