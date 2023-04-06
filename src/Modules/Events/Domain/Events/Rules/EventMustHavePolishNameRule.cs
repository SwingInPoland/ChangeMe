using ChangeMe.Modules.Events.Domain.ValueObjects.EventNameBase;
using ChangeMe.Shared.Domain;

namespace ChangeMe.Modules.Events.Domain.Events.Rules;

public class EventMustHavePolishNameRule : IBusinessRule
{
    private readonly List<EventNameBase> _names;

    public EventMustHavePolishNameRule(List<EventNameBase> names)
    {
        _names = names;
    }

    public bool IsBroken() => !_names.Any(n => n is PolishEventName);

    public string Message => "Event must have Polish name.";
}