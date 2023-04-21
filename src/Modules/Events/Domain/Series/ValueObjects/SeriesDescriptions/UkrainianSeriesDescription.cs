using ChangeMe.Modules.Events.Domain.ValueObjects.TranslationBase;

namespace ChangeMe.Modules.Events.Domain.Series.ValueObjects.SeriesDescriptions;

public class UkrainianSeriesDescription : PolishTranslationValue
{
    private UkrainianSeriesDescription(string value) : base(value) { }

    public static UkrainianSeriesDescription Create(string value) => new(value);
}