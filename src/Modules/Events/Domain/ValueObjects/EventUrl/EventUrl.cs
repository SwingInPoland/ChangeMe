using ChangeMe.Modules.Events.Domain.ValueObjects.EventUrl.Rules;
using ChangeMe.Shared.Domain;

namespace ChangeMe.Modules.Events.Domain.ValueObjects.EventUrl;

public class EventImage : ValueObject
{
    public Uri Value { get; }

    private EventImage(Uri value)
    {
        Value = value;
    }

    public static EventImage Create(Uri value)
    {
        CheckRule(new EventUrlMustBeBetween12And1000CharactersRule(value));
        CheckRule(new EventUrlSchemeMustBeValidRule(value));

        return new EventImage(value);
    }
}