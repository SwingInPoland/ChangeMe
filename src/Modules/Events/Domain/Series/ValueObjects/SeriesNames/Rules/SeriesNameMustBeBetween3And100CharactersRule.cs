using ChangeMe.Modules.Events.Domain.ValueObjects.TranslationBase;
using ChangeMe.Shared.Domain;

namespace ChangeMe.Modules.Events.Domain.Series.ValueObjects.SeriesNames.Rules;

public class SeriesNameMustBeBetween3And100CharactersRule : IBusinessRule
{
    private readonly IReadOnlyCollection<TranslationValueBase> _values;

    public SeriesNameMustBeBetween3And100CharactersRule(IReadOnlyCollection<TranslationValueBase> values)
    {
        _values = values;
    }

    public bool IsBroken() => _values.Any(v => v.Value.Length is < 3 or > 100);

    public string Message => "Series name must be between 3 and 100 characters.";
}