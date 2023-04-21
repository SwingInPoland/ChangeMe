using ChangeMe.Shared.Domain;

namespace ChangeMe.Modules.Events.Domain.ValueObjects.EventUrl.Rules;

public class EventUrlSchemeMustBeValidRule : IBusinessRule
{
    private readonly Uri _value;

    public EventUrlSchemeMustBeValidRule(Uri value)
    {
        _value = value;
    }

    public bool IsBroken() => _value.Scheme != Uri.UriSchemeHttp && _value.Scheme != Uri.UriSchemeHttps;

    public string Message => "Event URL scheme must be either 'http' or 'https'.";
}