using ChangeMe.Modules.Events.Domain.ValueObjects.EventNameBase.Rules;

namespace ChangeMe.Modules.Events.Domain.ValueObjects.EventNameBase;

public class UkrainianEventName : EventNameBase
{
    private UkrainianEventName(string value) : base(value) { }

    public static UkrainianEventName Create(string value)
    {
        CheckRule(new UkrainianEventNameMustBeBetween3And90CharactersRule(value));
        return new UkrainianEventName(value);
    }
}