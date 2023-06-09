using ChangeMe.Shared.Domain;

namespace ChangeMe.Modules.Events.Domain.Series.DomainEvents;

public class SeriesMainAttributesChangedDomainEvent : DomainEventBase
{
    public SeriesId SeriesId { get; }

    public SeriesMainAttributesChangedDomainEvent(SeriesId seriesId)
    {
        SeriesId = seriesId;
    }
}