using ChangeMe.Shared.Domain;

namespace ChangeMe.Modules.Events.Domain.ValueObjects.EventHost.Rules;

public class EventHostUrlSchemeMustBeValidRule : IBusinessRule
{
    private readonly Uri _url;

    public EventHostUrlSchemeMustBeValidRule(Uri url)
    {
        _url = url;
    }

    public bool IsBroken() => _url.Scheme != Uri.UriSchemeHttp && _url.Scheme != Uri.UriSchemeHttps;

    public string Message => "Event host URL scheme must be either 'http' or 'https'.";
}