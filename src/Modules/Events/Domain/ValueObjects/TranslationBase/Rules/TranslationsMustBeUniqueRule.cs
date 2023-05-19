using ChangeMe.Shared.Domain;

namespace ChangeMe.Modules.Events.Domain.ValueObjects.TranslationBase.Rules;

public class TranslationsMustBeUniqueRule : IBusinessRule
{
    private readonly IReadOnlyCollection<TranslationValueBase> _values;

    public TranslationsMustBeUniqueRule(IReadOnlyCollection<TranslationValueBase> values)
    {
        _values = values;
    }

    public bool IsBroken() => _values.GroupBy(v => v.GetType()).Any(g => g.Count() > 1);

    public string Message => "Translations must be unique.";
}