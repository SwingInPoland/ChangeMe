using ChangeMe.Shared.Domain;

namespace ChangeMe.Modules.Events.Domain.SingleEvent.DomainEvents;

public class SingleEventMainAttributesChangedDomainEvent : DomainEventBase
{
    public SingleEventId SingleEventId { get; }

    public SingleEventMainAttributesChangedDomainEvent(SingleEventId singleEventId)
    {
        SingleEventId = singleEventId;
    }
}