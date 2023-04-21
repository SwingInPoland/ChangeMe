using ChangeMe.Modules.Events.Domain.Series.Entities.SeriesEvent;
using ChangeMe.Shared.Domain;

namespace ChangeMe.Modules.Events.Domain.Series.DomainEvents;

public class SeriesEventMainAttributesChangedDomainEvent : DomainEventBase
{
    public SeriesEventId SeriesEventId { get; }

    public SeriesEventMainAttributesChangedDomainEvent(SeriesEventId seriesEventId)
    {
        SeriesEventId = seriesEventId;
    }
}