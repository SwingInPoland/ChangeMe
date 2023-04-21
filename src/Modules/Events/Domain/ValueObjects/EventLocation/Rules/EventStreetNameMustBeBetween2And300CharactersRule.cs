using ChangeMe.Shared.Domain;
using ChangeMe.Shared.Extensions;

namespace ChangeMe.Modules.Events.Domain.ValueObjects.EventLocation.Rules;

public class EventStreetNameMustBeBetween2And300CharactersRule : IBusinessRule
{
    private readonly string? _street;

    public EventStreetNameMustBeBetween2And300CharactersRule(string? street)
    {
        _street = street;
    }

    public bool IsBroken() => _street.IsNullOrWhiteSpace() || _street!.Length is < 2 or > 300;

    public string Message => "Event street name must be between 2 and 300 characters long.";
}