using ChangeMe.Modules.Events.Domain.ValueObjects.TranslationBase;
using ChangeMe.Shared.Domain;

namespace ChangeMe.Modules.Events.Domain.Series.ValueObjects.SeriesDescriptions.Rules;

public class SeriesDescriptionMustBeBetween3And1000CharactersRule : IBusinessRule
{
    private readonly IReadOnlyCollection<TranslationValueBase> _values;

    public SeriesDescriptionMustBeBetween3And1000CharactersRule(IReadOnlyCollection<TranslationValueBase> values)
    {
        _values = values;
    }

    public bool IsBroken() => _values.Any(v => v.Value.Length is < 10 or > 1000);

    public string Message => "Series description must be between 10 and 1000 characters.";
}