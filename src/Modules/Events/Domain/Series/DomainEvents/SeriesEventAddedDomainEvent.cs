using ChangeMe.Modules.Events.Domain.Series.Entities.SeriesEvent;
using ChangeMe.Shared.Domain;

namespace ChangeMe.Modules.Events.Domain.Series.DomainEvents;

public class SeriesEventAddedDomainEvent : DomainEventBase
{
    public SeriesId SeriesId { get; }
    public SeriesEventId SeriesEventId { get; }

    public SeriesEventAddedDomainEvent(SeriesId seriesId, SeriesEventId seriesEventId)
    {
        SeriesId = seriesId;
        SeriesEventId = seriesEventId;
    }
}