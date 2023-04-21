using ChangeMe.Shared.Domain;

namespace ChangeMe.Modules.Events.Domain.ValueObjects.EventImage.Rules;

public class EventUrlUrlSchemeMustBeValidRule : IBusinessRule
{
    private readonly Uri _url;

    public EventUrlUrlSchemeMustBeValidRule(Uri url)
    {
        _url = url;
    }

    public bool IsBroken() => _url.Scheme != Uri.UriSchemeHttp && _url.Scheme != Uri.UriSchemeHttps;

    public string Message => "Event image URL scheme must be either 'http' or 'https'.";
}