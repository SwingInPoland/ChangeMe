using ChangeMe.Shared.Domain;

namespace ChangeMe.Modules.Events.Domain.SingleEvent.DomainEvents;

public class UserRemovedFromSingleEventEditorsDomainEvent : DomainEventBase
{
    public SingleEventId SingleEventId { get; }

    public UserRemovedFromSingleEventEditorsDomainEvent(SingleEventId singleEventId)
    {
        SingleEventId = singleEventId;
    }
}