using ChangeMe.Modules.Events.Domain.ValueObjects.EventDescriptions.Rules;
using ChangeMe.Modules.Events.Domain.ValueObjects.TranslationBase;

namespace ChangeMe.Modules.Events.Domain.ValueObjects.EventDescriptions;

public class EventDescriptions : TranslationBase.TranslationBase
{
    private EventDescriptions(IReadOnlyCollection<TranslationValueBase> values) : base(values) { }

    public static EventDescriptions Create(IReadOnlyCollection<TranslationValueBase> values)
    {
        CheckRule(new EventDescriptionsMustBeBetween10And1000CharactersRule(values));
        return new EventDescriptions(values);
    }
}