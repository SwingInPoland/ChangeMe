using ChangeMe.Modules.Events.Domain.Series.Entities.SeriesEvent;
using ChangeMe.Shared.Domain;

namespace ChangeMe.Modules.Events.Domain.Series.DomainEvents;

public class UserRemovedFromSeriesEventEditorsDomainEvent : DomainEventBase
{
    public SeriesEventId SeriesEventId { get; }

    public UserRemovedFromSeriesEventEditorsDomainEvent(SeriesEventId seriesEventId)
    {
        SeriesEventId = seriesEventId;
    }
}