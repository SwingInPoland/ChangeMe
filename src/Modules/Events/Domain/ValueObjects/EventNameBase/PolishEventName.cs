using ChangeMe.Modules.Events.Domain.ValueObjects.EventNameBase.Rules;

namespace ChangeMe.Modules.Events.Domain.ValueObjects.EventNameBase;

public class PolishEventName : EventNameBase
{
    private PolishEventName(string value) : base(value) { }

    public static PolishEventName Create(string value)
    {
        CheckRule(new PolishEventNameMustBeBetween3And100CharactersRule(value));
        return new PolishEventName(value);
    }
}