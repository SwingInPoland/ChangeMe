using ChangeMe.Modules.Events.Domain.ValueObjects.TranslationBase;

namespace ChangeMe.Modules.Events.Domain.ValueObjects.EventDescriptions;

public class PolishEventDescription : PolishTranslationValue
{
    private PolishEventDescription(string value) : base(value) { }

    public static PolishEventDescription Create(string value) => new(value);
}