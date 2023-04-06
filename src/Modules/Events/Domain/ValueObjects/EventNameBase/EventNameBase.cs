using ChangeMe.Shared.Domain;

namespace ChangeMe.Modules.Events.Domain.ValueObjects.EventNameBase;

public abstract class EventNameBase : ValueObject
{
    public string Value { get; }

    protected EventNameBase(string value)
    {
        Value = value;
    }
}