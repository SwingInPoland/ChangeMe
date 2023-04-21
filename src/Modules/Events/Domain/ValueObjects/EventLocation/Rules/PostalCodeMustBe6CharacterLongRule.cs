using ChangeMe.Shared.Domain;
using ChangeMe.Shared.Extensions;

namespace ChangeMe.Modules.Events.Domain.ValueObjects.EventLocation.Rules;

public class PostalCodeMustBe6CharacterLongRule : IBusinessRule
{
    private readonly string _postalCode;

    public PostalCodeMustBe6CharacterLongRule(string postalCode)
    {
        _postalCode = postalCode;
    }

    public bool IsBroken() => _postalCode.IsNullOrWhiteSpace() || _postalCode.Length != 6;

    public string Message => "Postal code must be 6 characters long.";
}