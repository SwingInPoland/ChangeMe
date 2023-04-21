using ChangeMe.Shared.Domain;

namespace ChangeMe.Modules.Events.Domain.Series.Rules;

public class SeriesDeletedDomainEvent : DomainEventBase
{
    public SeriesId SeriesId { get; }

    public SeriesDeletedDomainEvent(SeriesId seriesId)
    {
        SeriesId = seriesId;
    }
}