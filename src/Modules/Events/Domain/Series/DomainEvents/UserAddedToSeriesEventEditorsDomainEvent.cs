using ChangeMe.Modules.Events.Domain.Series.Entities.SeriesEvent;
using ChangeMe.Shared.Domain;

namespace ChangeMe.Modules.Events.Domain.Series.DomainEvents;

public class UserAddedToSeriesEventEditorsDomainEvent : DomainEventBase
{
    public SeriesEventId SeriesEventId { get; }

    public UserAddedToSeriesEventEditorsDomainEvent(SeriesEventId seriesEventId)
    {
        SeriesEventId = seriesEventId;
    }
}