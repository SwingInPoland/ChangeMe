using ChangeMe.Shared.Domain;

namespace ChangeMe.Modules.Events.Domain.Events.DomainEvents;

public class EventCreatedDomainEvent : DomainEventBase
{
    public EventId EventId { get; }

    public EventCreatedDomainEvent(EventId eventId)
    {
        EventId = eventId;
    }
}