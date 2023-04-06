using ChangeMe.Modules.Events.Domain.Events;
using ChangeMe.Shared.Domain;

namespace ChangeMe.Modules.Events.Domain.Series.DomainEvents;

public class SeriesEventCreatedDomainEvent : DomainEventBase
{
    public EventId EventId { get; }

    public SeriesEventCreatedDomainEvent(EventId eventId)
    {
        EventId = eventId;
    }
}