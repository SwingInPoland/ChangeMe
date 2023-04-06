using ChangeMe.Modules.Events.Domain.Events.DomainEvents;
using ChangeMe.Modules.Events.Domain.Events.Rules;
using ChangeMe.Modules.Events.Domain.Events.ValueObjects.EventDate;
using ChangeMe.Modules.Events.Domain.ValueObjects.EventDateBase;
using ChangeMe.Modules.Events.Domain.ValueObjects.EventNameBase;
using ChangeMe.Shared.Domain;

namespace ChangeMe.Modules.Events.Domain.Events;

public class Event : EventBase, IAggregateRoot
{
    private Event(List<EventNameBase> names, EventDateBase date) : base(names, date)
    {
        AddDomainEvent(new EventCreatedDomainEvent(EventId));
    }
    
    public static Event Create(List<EventNameBase> names, EventDate date)
    {
        CheckRule(new EventMustHavePolishNameRule(names));
        return new Event(names, date);
    }
}