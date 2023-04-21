using ChangeMe.Modules.Events.Domain.ValueObjects.TranslationBase;

namespace ChangeMe.Modules.Events.Domain.Series.ValueObjects.SeriesDescriptions;

public class EnglishSeriesDescription : PolishTranslationValue
{
    private EnglishSeriesDescription(string value) : base(value) { }

    public static EnglishSeriesDescription Create(string value) => new(value);
}