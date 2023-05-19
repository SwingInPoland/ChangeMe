using ChangeMe.Modules.Events.Domain.Series;

namespace ChangeMe.Modules.Events.Infrastructure.Domain.Series;

public class SeriesRepository : ISeriesRepository
{
    private readonly EventsContext _eventsContext;

    public SeriesRepository(EventsContext eventsContext)
    {
        _eventsContext = eventsContext;
    }

    public async Task AddAsync(
        Events.Domain.Series.Series singleEvent,
        CancellationToken cancellationToken) =>
        await _eventsContext.Series.AddAsync(singleEvent, cancellationToken);

    public async Task<Events.Domain.Series.Series?> GetByIdAsync(
        SeriesId id,
        CancellationToken cancellationToken) =>
        await _eventsContext.Series.FindAsync(new object?[] { id }, cancellationToken);
}