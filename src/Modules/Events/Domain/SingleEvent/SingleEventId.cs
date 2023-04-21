using ChangeMe.Shared.Domain;

namespace ChangeMe.Modules.Events.Domain.SingleEvent;

public class SingleEventId : TypedIdValueBase
{
    public SingleEventId(Guid value) : base(value) { }
}