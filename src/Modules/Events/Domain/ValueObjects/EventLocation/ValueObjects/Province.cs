using ChangeMe.Modules.Events.Domain.ValueObjects.EventLocation.Rules;
using ChangeMe.Shared.Domain;

namespace ChangeMe.Modules.Events.Domain.ValueObjects.EventLocation.ValueObjects;

public class Province : ValueObject
{
    public string Value { get; }

    private Province(string value)
    {
        Value = value;
    }

    public static Province Create(string value)
    {
        CheckRule(new ProvinceMustExistsRule(value));

        return new Province(value);
    }
}