using ChangeMe.Modules.Events.Domain.Series.Entities.SeriesEvent;
using ChangeMe.Shared.Domain;

namespace ChangeMe.Modules.Events.Domain.Series.DomainEvents;

public class SeriesEventEditorsChangedDomainEvent : DomainEventBase
{
    public SeriesEventId SeriesEventId { get; }

    public SeriesEventEditorsChangedDomainEvent(SeriesEventId seriesEventId)
    {
        SeriesEventId = seriesEventId;
    }
}