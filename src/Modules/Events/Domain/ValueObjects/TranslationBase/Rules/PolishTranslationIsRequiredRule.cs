using ChangeMe.Shared.Domain;

namespace ChangeMe.Modules.Events.Domain.ValueObjects.TranslationBase.Rules;

public class PolishTranslationIsRequiredRule : IBusinessRule
{
    private readonly IReadOnlyCollection<TranslationValueBase> _values;

    public PolishTranslationIsRequiredRule(IReadOnlyCollection<TranslationValueBase> values)
    {
        _values = values;
    }

    public bool IsBroken() => !_values.Any(v => v is PolishTranslationValue);

    public string Message => "Polish translation is required.";
}