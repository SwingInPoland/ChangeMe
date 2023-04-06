using ChangeMe.Shared.Domain;

namespace ChangeMe.Modules.Events.Domain.Series.DomainEvents;

public class SeriesCreatedDomainEvent : DomainEventBase
{
    public SeriesId SeriesId { get; }

    public SeriesCreatedDomainEvent(SeriesId seriesId)
    {
        SeriesId = seriesId;
    }
}