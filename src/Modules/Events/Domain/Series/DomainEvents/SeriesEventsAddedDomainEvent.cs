using ChangeMe.Modules.Events.Domain.Series.Entities.SeriesEvent;
using ChangeMe.Shared.Domain;
using ChangeMe.Shared.Extensions;

namespace ChangeMe.Modules.Events.Domain.Series.DomainEvents;

public class SeriesEventsAddedDomainEvent : DomainEventBase
{
    public SeriesId SeriesId { get; }
    public IReadOnlyCollection<SeriesEventId> SeriesEventsIds { get; }

    public SeriesEventsAddedDomainEvent(SeriesId seriesId, IEnumerable<SeriesEventId> seriesEventsIds)
    {
        SeriesId = seriesId;
        SeriesEventsIds = seriesEventsIds.ToReadOnly();
    }
}