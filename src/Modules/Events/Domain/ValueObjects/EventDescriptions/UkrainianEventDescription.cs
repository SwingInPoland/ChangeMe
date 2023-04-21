using ChangeMe.Modules.Events.Domain.ValueObjects.TranslationBase;

namespace ChangeMe.Modules.Events.Domain.ValueObjects.EventDescriptions;

public class UkrainianEventDescription : PolishTranslationValue
{
    private UkrainianEventDescription(string value) : base(value) { }

    public static UkrainianEventDescription Create(string value) => new(value);
}