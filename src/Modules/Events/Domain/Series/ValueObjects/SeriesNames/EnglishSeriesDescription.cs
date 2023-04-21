using ChangeMe.Modules.Events.Domain.ValueObjects.TranslationBase;

namespace ChangeMe.Modules.Events.Domain.Series.ValueObjects.SeriesNames;

public class EnglishSeriesName : PolishTranslationValue
{
    private EnglishSeriesName(string value) : base(value) { }

    public static EnglishSeriesName Create(string value) => new(value);
}