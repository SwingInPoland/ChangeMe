using ChangeMe.Shared.Domain;

namespace ChangeMe.Modules.Events.Domain.Events;

public class EventId : TypedIdValueBase
{
    public EventId(Guid value) : base(value) { }
}