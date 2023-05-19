namespace ChangeMe.Shared.Infrastructure.UnitOfWork;

public interface IUnitOfWork
{
    Task<int> CommitAsync(Guid? internalCommandId = null, CancellationToken cancellationToken = default);
}