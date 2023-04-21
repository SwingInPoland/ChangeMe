using ChangeMe.Modules.Events.Domain.ValueObjects.EventNames.Rules;
using ChangeMe.Modules.Events.Domain.ValueObjects.TranslationBase;

namespace ChangeMe.Modules.Events.Domain.ValueObjects.EventNames;

public class UkrainianEventName : UkrainianTranslationValue
{
    private UkrainianEventName(string value) : base(value) { }

    public static UkrainianEventName Create(string value)
    {
        CheckRule(new UkrainianEventNameMustBeBetween3And90CharactersRule(value));
        return new UkrainianEventName(value);
    }
}