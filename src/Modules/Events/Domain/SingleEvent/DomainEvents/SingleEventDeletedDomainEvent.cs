using ChangeMe.Shared.Domain;

namespace ChangeMe.Modules.Events.Domain.SingleEvent.DomainEvents;

public class SingleEventDeletedDomainEvent : DomainEventBase
{
    public SingleEventId SingleEventId { get; }

    public SingleEventDeletedDomainEvent(SingleEventId singleEventId)
    {
        SingleEventId = singleEventId;
    }
}