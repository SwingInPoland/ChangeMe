using ChangeMe.Modules.Events.Domain.Series.ValueObjects.SeriesDescriptions.Rules;
using ChangeMe.Modules.Events.Domain.ValueObjects.TranslationBase;

namespace ChangeMe.Modules.Events.Domain.Series.ValueObjects.SeriesDescriptions;

public class SeriesDescriptions : TranslationBase
{
    private SeriesDescriptions(IReadOnlyCollection<TranslationValueBase> values) : base(values) { }

    public static SeriesDescriptions Create(IReadOnlyCollection<TranslationValueBase> values)
    {
        CheckRule(new SeriesDescriptionMustBeBetween3And1000CharactersRule(values));
        return new SeriesDescriptions(values);
    }
}