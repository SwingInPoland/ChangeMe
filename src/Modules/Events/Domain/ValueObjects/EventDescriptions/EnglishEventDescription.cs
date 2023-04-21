using ChangeMe.Modules.Events.Domain.ValueObjects.TranslationBase;

namespace ChangeMe.Modules.Events.Domain.ValueObjects.EventDescriptions;

public class EnglishEventDescription : PolishTranslationValue
{
    private EnglishEventDescription(string value) : base(value) { }

    public static EnglishEventDescription Create(string value) => new(value);
}