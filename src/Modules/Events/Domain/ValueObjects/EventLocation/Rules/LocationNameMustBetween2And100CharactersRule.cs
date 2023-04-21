using ChangeMe.Shared.Domain;
using ChangeMe.Shared.Extensions;

namespace ChangeMe.Modules.Events.Domain.ValueObjects.EventLocation.Rules;

public class LocationNameMustBetween2And100CharactersRule : IBusinessRule
{
    private readonly string _name;

    public LocationNameMustBetween2And100CharactersRule(string name)
    {
        _name = name;
    }

    public bool IsBroken() => _name.IsNullOrWhiteSpace() || _name.Length is < 2 or > 100;

    public string Message => "Location name must be between 2 and 100 characters long.";
}