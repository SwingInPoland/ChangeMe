using ChangeMe.Modules.Events.Domain.ValueObjects.TranslationBase;

namespace ChangeMe.Modules.Events.Domain.Series.ValueObjects.SeriesNames;

public class PolishSeriesName : PolishTranslationValue
{
    private PolishSeriesName(string value) : base(value) { }

    public static PolishSeriesName Create(string value) => new(value);
}