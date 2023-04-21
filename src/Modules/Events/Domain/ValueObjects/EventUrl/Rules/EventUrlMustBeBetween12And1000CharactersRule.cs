using ChangeMe.Shared.Domain;

namespace ChangeMe.Modules.Events.Domain.ValueObjects.EventUrl.Rules;

public class EventUrlMustBeBetween12And1000CharactersRule : IBusinessRule
{
    private readonly Uri _value;

    public EventUrlMustBeBetween12And1000CharactersRule(Uri value)
    {
        _value = value;
    }

    public bool IsBroken() => _value.OriginalString.Length is < 12 or > 1000;

    public string Message => "Event URL must be between 12 and 1000 characters long.";
}