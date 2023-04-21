using ChangeMe.Modules.Events.Domain.Series.Entities.SeriesEvent;
using ChangeMe.Shared.Domain;

namespace ChangeMe.Modules.Events.Domain.Series.DomainEvents;

public class SeriesEventStatusChangedDomainEvent : DomainEventBase
{
    public SeriesEventId SeriesEventId { get; }

    public SeriesEventStatusChangedDomainEvent(SeriesEventId seriesEventId)
    {
        SeriesEventId = seriesEventId;
    }
}