using ChangeMe.Shared.Domain;

namespace ChangeMe.Modules.Events.Domain.SingleEvent.DomainEvents;

public class SingleEventEditorsChangedDomainEvent : DomainEventBase
{
    public SingleEventId SingleEventId { get; }

    public SingleEventEditorsChangedDomainEvent(SingleEventId singleEventId)
    {
        SingleEventId = singleEventId;
    }
}