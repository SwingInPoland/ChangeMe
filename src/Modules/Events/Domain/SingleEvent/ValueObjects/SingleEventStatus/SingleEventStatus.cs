using ChangeMe.Modules.Events.Domain.ValueObjects.EventStatusBase;

namespace ChangeMe.Modules.Events.Domain.SingleEvent.ValueObjects.SingleEventStatus;

public class SingleEventStatus : EventStatusBase
{
    private SingleEventStatus(string value = EventStatuses.Planning) : base(value) { }

    public static SingleEventStatus Create() => new();

    public static SingleEventStatus Create(string value) => new(value);

    internal override SingleEventStatus ChangeStatus(string value) => new(value);
}