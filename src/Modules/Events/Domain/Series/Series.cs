using ChangeMe.Modules.Events.Domain.Series.DomainEvents;
using ChangeMe.Modules.Events.Domain.Series.Entities.SeriesEvent;
using ChangeMe.Shared.Domain;

namespace ChangeMe.Modules.Events.Domain.Series;

public class Series : Entity, IAggregateRoot
{
    public SeriesId SeriesId { get; }
    private List<SeriesEvent> _events;

    private Series(List<SeriesEvent> events)
    {
        SeriesId = new SeriesId(Guid.NewGuid());
        _events = events;
        
        AddDomainEvent(new SeriesCreatedDomainEvent(SeriesId));
    }
    
    public static Series Create(List<SeriesEvent> events)
    {
        return new Series(events);
    }
}