using ChangeMe.Modules.Events.Domain.Series.ValueObjects.SeriesNames.Rules;
using ChangeMe.Modules.Events.Domain.ValueObjects.TranslationBase;

namespace ChangeMe.Modules.Events.Domain.Series.ValueObjects.SeriesNames;

public class SeriesNames : TranslationBase
{
    private SeriesNames(IReadOnlyCollection<TranslationValueBase> values) : base(values) { }

    public static SeriesNames Create(IReadOnlyCollection<TranslationValueBase> values)
    {
        CheckRule(new SeriesNameMustBeBetween3And100CharactersRule(values));
        return new SeriesNames(values);
    }
}