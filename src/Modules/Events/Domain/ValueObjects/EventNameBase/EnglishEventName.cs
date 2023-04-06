using ChangeMe.Modules.Events.Domain.ValueObjects.EventNameBase.Rules;

namespace ChangeMe.Modules.Events.Domain.ValueObjects.EventNameBase;

public class EnglishEventName : EventNameBase
{
    private EnglishEventName(string value) : base(value) { }

    public static EnglishEventName Create(string value)
    {
        CheckRule(new EnglishEventNameMustBeBetween3And80CharactersRule(value));
        return new EnglishEventName(value);
    }
}