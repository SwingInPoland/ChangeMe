using ChangeMe.Modules.Events.Domain.ValueObjects.EventLocation.Rules;
using ChangeMe.Shared.Domain;

namespace ChangeMe.Modules.Events.Domain.ValueObjects.EventLocation.ValueObjects;

public class LocationName : ValueObject
{
    public string Value { get; }

    private LocationName(string value)
    {
        CheckRule(new LocationNameMustBetween2And100CharactersRule(value));
        Value = value;
    }

    public static LocationName? TryCreate(string? value)
    {
        if (value is null)
            return null;

        CheckRule(new LocationNameMustBetween2And100CharactersRule(value));

        return new LocationName(value);
    }
}