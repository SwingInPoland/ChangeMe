using ChangeMe.Modules.Events.Domain.Series.Entities.SeriesEvent;
using ChangeMe.Shared.Domain;

namespace ChangeMe.Modules.Events.Domain.Series.DomainEvents;

public class SeriesEventRemovedDomainEvent : DomainEventBase
{
    public SeriesId SeriesId { get; }
    public SeriesEventId SeriesEventId { get; }

    public SeriesEventRemovedDomainEvent(SeriesId seriesId, SeriesEventId seriesEventId)
    {
        SeriesId = seriesId;
        SeriesEventId = seriesEventId;
    }
}