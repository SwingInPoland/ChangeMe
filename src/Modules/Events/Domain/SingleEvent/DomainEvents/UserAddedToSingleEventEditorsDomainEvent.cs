using ChangeMe.Shared.Domain;

namespace ChangeMe.Modules.Events.Domain.SingleEvent.DomainEvents;

public class UserAddedToSingleEventEditorsDomainEvent : DomainEventBase
{
    public SingleEventId SingleEventId { get; }

    public UserAddedToSingleEventEditorsDomainEvent(SingleEventId singleEventId)
    {
        SingleEventId = singleEventId;
    }
}