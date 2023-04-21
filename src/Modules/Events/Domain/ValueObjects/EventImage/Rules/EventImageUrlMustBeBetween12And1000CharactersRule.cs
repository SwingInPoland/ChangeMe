using ChangeMe.Shared.Domain;

namespace ChangeMe.Modules.Events.Domain.ValueObjects.EventImage.Rules;

public class EventUrlUrlMustBeBetween12And1000CharactersRule : IBusinessRule
{
    private readonly Uri _url;

    public EventUrlUrlMustBeBetween12And1000CharactersRule(Uri url)
    {
        _url = url;
    }

    public bool IsBroken() => _url.OriginalString.Length is < 12 or > 1000;

    public string Message => "Event image URL must be between 12 and 1000 characters long.";
}