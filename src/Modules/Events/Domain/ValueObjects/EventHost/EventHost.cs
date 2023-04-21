using ChangeMe.Modules.Events.Domain.ValueObjects.EventHost.Rules;
using ChangeMe.Shared.Domain;

namespace ChangeMe.Modules.Events.Domain.ValueObjects.EventHost;

public class EventHost : ValueObject
{
    public string Name { get; }
    public Uri? Url { get; }

    private EventHost(string name, Uri? url)
    {
        Name = name;
        Url = url;
    }

    public static EventHost Create(string name, Uri? url)
    {
        CheckRule(new EventHostNameMustBeBetween3And100CharactersRule(name));

        if (url is not null)
        {
            CheckRule(new EventHostUrlMustBeBetween12And400CharactersRule(url));
            CheckRule(new EventHostUrlSchemeMustBeValidRule(url));
        }

        return new EventHost(name, url);
    }
}