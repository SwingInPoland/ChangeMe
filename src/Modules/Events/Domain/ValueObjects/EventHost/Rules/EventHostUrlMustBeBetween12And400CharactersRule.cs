using ChangeMe.Shared.Domain;

namespace ChangeMe.Modules.Events.Domain.ValueObjects.EventHost.Rules;

public class EventHostUrlMustBeBetween12And400CharactersRule : IBusinessRule
{
    private readonly Uri _url;

    public EventHostUrlMustBeBetween12And400CharactersRule(Uri url)
    {
        _url = url;
    }

    public bool IsBroken() => _url.OriginalString.Length is < 12 or > 400;

    public string Message => "Event host URL must be between 12 and 400 characters long.";
}