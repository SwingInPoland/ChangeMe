namespace ChangeMe.Shared.Application;

public interface IExecutionContextAccessor
{
    string UserId { get; }

    Guid CorrelationId { get; }

    bool IsAvailable { get; }
}