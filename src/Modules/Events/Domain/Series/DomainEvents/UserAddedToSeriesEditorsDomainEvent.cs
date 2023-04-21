using ChangeMe.Shared.Domain;

namespace ChangeMe.Modules.Events.Domain.Series.DomainEvents;

public class UserAddedToSeriesEditorsDomainEvent : DomainEventBase
{
    public SeriesId SeriesId { get; }

    public UserAddedToSeriesEditorsDomainEvent(SeriesId seriesId)
    {
        SeriesId = seriesId;
    }
}