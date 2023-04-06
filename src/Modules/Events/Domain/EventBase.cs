using ChangeMe.Modules.Events.Domain.Events;
using ChangeMe.Modules.Events.Domain.ValueObjects.EventDateBase;
using ChangeMe.Modules.Events.Domain.ValueObjects.EventNameBase;
using ChangeMe.Shared.Domain;

namespace ChangeMe.Modules.Events.Domain;

public abstract class EventBase : Entity
{
    public EventId EventId { get; }
    private List<EventNameBase> _names;
    private EventDateBase _date;

    protected EventBase(List<EventNameBase> names, EventDateBase date)
    {
        EventId = new EventId(Guid.NewGuid());
        _names = names;
        _date = date;
    }
}