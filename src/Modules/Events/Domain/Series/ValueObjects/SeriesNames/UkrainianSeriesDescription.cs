using ChangeMe.Modules.Events.Domain.ValueObjects.TranslationBase;

namespace ChangeMe.Modules.Events.Domain.Series.ValueObjects.SeriesNames;

public class UkrainianSeriesName : PolishTranslationValue
{
    private UkrainianSeriesName(string value) : base(value) { }

    public static UkrainianSeriesName Create(string value) => new(value);
}