using ChangeMe.Shared.Domain;

namespace ChangeMe.Modules.Events.Domain.Series.DomainEvents;

public class SeriesEditorsChangedDomainEvent : DomainEventBase
{
    public SeriesId SeriesId { get; }

    public SeriesEditorsChangedDomainEvent(SeriesId seriesId)
    {
        SeriesId = seriesId;
    }
}