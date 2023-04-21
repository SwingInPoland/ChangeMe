using ChangeMe.Modules.Events.Domain.ValueObjects.EventImage.Rules;
using ChangeMe.Shared.Domain;

namespace ChangeMe.Modules.Events.Domain.ValueObjects.EventImage;

public class EventUrl : ValueObject
{
    public Uri Url { get; }

    private EventUrl(Uri url)
    {
        Url = url;
    }

    public static EventUrl Create(Uri url)
    {
        CheckRule(new EventUrlUrlMustBeBetween12And1000CharactersRule(url));
        CheckRule(new EventUrlUrlSchemeMustBeValidRule(url));

        return new EventUrl(url);
    }
}