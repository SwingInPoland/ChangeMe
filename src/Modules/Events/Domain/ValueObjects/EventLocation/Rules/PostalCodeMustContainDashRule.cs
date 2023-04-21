using ChangeMe.Shared.Domain;
using ChangeMe.Shared.Extensions;

namespace ChangeMe.Modules.Events.Domain.ValueObjects.EventLocation.Rules;

public class PostalCodeMustContainDashRule : IBusinessRule
{
    private readonly string _postalCode;

    public PostalCodeMustContainDashRule(string postalCode)
    {
        _postalCode = postalCode;
    }

    public bool IsBroken() => _postalCode.IsNullOrWhiteSpace() || _postalCode.ElementAt(2) != '-';

    public string Message => "Postal code must contain a dash.";
}