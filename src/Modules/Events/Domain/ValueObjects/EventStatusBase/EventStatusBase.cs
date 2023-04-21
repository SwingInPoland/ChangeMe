using ChangeMe.Shared.Domain;

namespace ChangeMe.Modules.Events.Domain.ValueObjects.EventStatusBase;

public abstract class EventStatusBase : ValueObject
{
    public string Value { get; }

    protected EventStatusBase(string value = EventStatuses.Planning)
    {
        Value = value;
    }

    internal abstract EventStatusBase ChangeStatus(string value);
}

public static class EventStatuses
{
    public const string Confirmed = "Confirmed";
    public const string Cancelled = "Cancelled";
    public const string Planning = "Planning";
}