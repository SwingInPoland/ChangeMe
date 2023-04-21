using ChangeMe.Modules.Events.Domain.ValueObjects.EventLocation.Rules;
using ChangeMe.Shared.Domain;

namespace ChangeMe.Modules.Events.Domain.ValueObjects.EventLocation.ValueObjects;

public class PostalCode : ValueObject
{
    public string Value { get; }

    private PostalCode(string value)
    {
        Value = value;
    }

    public static PostalCode Create(string value)
    {
        CheckRule(new PostalCodeMustBe6CharacterLongRule(value));
        CheckRule(new PostalCodeMustContainDashRule(value));

        return new PostalCode(value);
    }
}