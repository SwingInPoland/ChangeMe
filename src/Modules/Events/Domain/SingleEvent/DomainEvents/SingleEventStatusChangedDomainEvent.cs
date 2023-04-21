using ChangeMe.Shared.Domain;

namespace ChangeMe.Modules.Events.Domain.SingleEvent.DomainEvents;

public class SingleEventStatusChangedDomainEvent : DomainEventBase
{
    public SingleEventId SingleEventId { get; }

    public SingleEventStatusChangedDomainEvent(SingleEventId singleEventId)
    {
        SingleEventId = singleEventId;
    }
}