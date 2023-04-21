using ChangeMe.Shared.Domain;

namespace ChangeMe.Modules.Events.Domain.SingleEvent.DomainEvents;

public class SingleEventCreatedDomainEvent : DomainEventBase
{
    public SingleEventId SingleEventId { get; }

    public SingleEventCreatedDomainEvent(SingleEventId singleEventId)
    {
        SingleEventId = singleEventId;
    }
}