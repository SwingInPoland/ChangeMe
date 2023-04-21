using ChangeMe.Shared.Domain;
using ChangeMe.Shared.Extensions;

namespace ChangeMe.Modules.Events.Domain.ValueObjects.EventLocation.Rules;

public class CityMustBeBetween4And50CharactersRule : IBusinessRule
{
    private readonly string _value;

    public CityMustBeBetween4And50CharactersRule(string value)
    {
        _value = value;
    }

    public bool IsBroken() => _value.IsNullOrWhiteSpace() || _value.Length is < 4 or > 50;

    public string Message => "City must be between 4 and 50 characters long.";
}