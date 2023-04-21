using ChangeMe.Modules.Events.Domain.ValueObjects.TranslationBase;

namespace ChangeMe.Modules.Events.Domain.Series.ValueObjects.SeriesDescriptions;

public class PolishSeriesDescription : PolishTranslationValue
{
    private PolishSeriesDescription(string value) : base(value) { }

    public static PolishSeriesDescription Create(string value) => new(value);
}