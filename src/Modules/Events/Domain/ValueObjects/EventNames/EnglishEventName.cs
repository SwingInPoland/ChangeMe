using ChangeMe.Modules.Events.Domain.ValueObjects.EventNames.Rules;
using ChangeMe.Modules.Events.Domain.ValueObjects.TranslationBase;

namespace ChangeMe.Modules.Events.Domain.ValueObjects.EventNames;

public class EnglishEventName : EnglishTranslationValue
{
    private EnglishEventName(string value) : base(value) { }

    public static EnglishEventName Create(string value)
    {
        CheckRule(new EnglishEventNameMustBeBetween3And80CharactersRule(value));
        return new EnglishEventName(value);
    }
}