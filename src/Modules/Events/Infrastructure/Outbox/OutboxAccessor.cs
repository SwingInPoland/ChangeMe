using ChangeMe.Shared.Application.Outbox;

namespace ChangeMe.Modules.Events.Infrastructure.Outbox;

public class OutboxAccessor : IOutbox
{
    private readonly EventsContext _eventsContext;

    internal OutboxAccessor(EventsContext eventsContext)
    {
        _eventsContext = eventsContext;
    }

    public void Add(OutboxMessage message) => _eventsContext.OutboxMessages.Add(message);

    // Save is done automatically using EF Core Change Tracking mechanism during SaveChanges.
    public Task SaveAsync() => Task.CompletedTask;
}