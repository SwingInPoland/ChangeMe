using ChangeMe.Modules.Events.Domain.ValueObjects.EventNames.Rules;
using ChangeMe.Modules.Events.Domain.ValueObjects.TranslationBase;

namespace ChangeMe.Modules.Events.Domain.ValueObjects.EventNames;

public class PolishEventName : PolishTranslationValue
{
    private PolishEventName(string value) : base(value) { }

    public static PolishEventName Create(string value)
    {
        CheckRule(new PolishEventNameMustBeBetween3And100CharactersRule(value));
        return new PolishEventName(value);
    }
}