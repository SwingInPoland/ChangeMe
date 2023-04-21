using ChangeMe.Shared.Domain;

namespace ChangeMe.Modules.Events.Domain.ValueObjects.TranslationBase;

public abstract class TranslationValueBase : ValueObject
{
    public string Value { get; }

    protected TranslationValueBase(string value)
    {
        Value = value;
    }
}