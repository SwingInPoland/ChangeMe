using ChangeMe.Shared.Domain;

namespace ChangeMe.Modules.Events.Domain.Series.DomainEvents;

public class UserRemovedFromSeriesEditorsDomainEvent : DomainEventBase
{
    public SeriesId SeriesId { get; }

    public UserRemovedFromSeriesEditorsDomainEvent(SeriesId seriesId)
    {
        SeriesId = seriesId;
    }
}