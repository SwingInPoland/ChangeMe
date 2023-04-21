using ChangeMe.Modules.Events.Domain.ValueObjects.TranslationBase;

namespace ChangeMe.Modules.Events.Domain.ValueObjects.EventNames;

public class EventNames : TranslationBase.TranslationBase
{
    private EventNames(IReadOnlyCollection<TranslationValueBase> values) : base(values) { }

    public static EventNames Create(IReadOnlyCollection<TranslationValueBase> values) => new(values);
}