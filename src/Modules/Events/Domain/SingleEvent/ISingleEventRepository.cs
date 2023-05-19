namespace ChangeMe.Modules.Events.Domain.SingleEvent;

public interface ISingleEventRepository
{
    Task AddAsync(SingleEvent singleEvent, CancellationToken cancellationToken);

    Task<SingleEvent?> GetByIdAsync(SingleEventId id, CancellationToken cancellationToken);
}