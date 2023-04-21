using ChangeMe.Modules.Events.Domain.ValueObjects.EventLocation.Rules;
using ChangeMe.Shared.Domain;

namespace ChangeMe.Modules.Events.Domain.ValueObjects.EventLocation.ValueObjects;

public class City : ValueObject
{
    public string Value { get; }

    private City(string value)
    {
        Value = value;
    }

    public static City Create(string value)
    {
        CheckRule(new CityMustBeBetween4And50CharactersRule(value));

        return new City(value);
    }
}