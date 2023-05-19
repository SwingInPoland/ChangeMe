using ChangeMe.Modules.Events.Domain.SingleEvent;

namespace ChangeMe.Modules.Events.Infrastructure.Domain.SingleEvent;

public class SingleEventRepository : ISingleEventRepository
{
    private readonly EventsContext _eventsContext;

    public SingleEventRepository(EventsContext eventsContext)
    {
        _eventsContext = eventsContext;
    }

    public async Task AddAsync(
        Events.Domain.SingleEvent.SingleEvent singleEvent,
        CancellationToken cancellationToken) =>
        await _eventsContext.SingleEvents.AddAsync(singleEvent, cancellationToken);

    public async Task<Events.Domain.SingleEvent.SingleEvent?> GetByIdAsync(
        SingleEventId id,
        CancellationToken cancellationToken) =>
        await _eventsContext.SingleEvents.FindAsync(new object?[] { id }, cancellationToken);
}