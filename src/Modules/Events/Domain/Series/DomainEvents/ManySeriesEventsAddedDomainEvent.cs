using ChangeMe.Modules.Events.Domain.Series.Entities.SeriesEvent;
using ChangeMe.Shared.Domain;

namespace ChangeMe.Modules.Events.Domain.Series.DomainEvents;

public class ManySeriesEventsAddedDomainEvent : DomainEventBase
{
    public SeriesId SeriesId { get; }
    public IReadOnlyCollection<SeriesEventId> SeriesEventsIds { get; }

    public ManySeriesEventsAddedDomainEvent(SeriesId seriesId, IReadOnlyCollection<SeriesEventId> seriesEventsIds)
    {
        SeriesId = seriesId;
        SeriesEventsIds = seriesEventsIds;
    }
}