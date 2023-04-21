using ChangeMe.Modules.Events.Domain.ValueObjects.TranslationBase.Rules;
using ChangeMe.Shared.Domain;

namespace ChangeMe.Modules.Events.Domain.ValueObjects.TranslationBase;

public abstract class TranslationBase : ValueObject
{
    public IReadOnlyCollection<TranslationValueBase> Values { get; }

    protected TranslationBase(IReadOnlyCollection<TranslationValueBase> values)
    {
        CheckRule(new PolishTranslationIsRequiredRule(values));
        Values = values;
    }
}