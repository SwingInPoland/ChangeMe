using ChangeMe.Shared.Domain;
using ChangeMe.Shared.Extensions;

namespace ChangeMe.Modules.Events.Domain.ValueObjects.EventLocation.Rules;

public class EventStreetNumberMustBeProvidedInOrderToAdditionalInfoRule : IBusinessRule
{
    private readonly string? _streetNumber;
    private readonly string? _additionalInfo;

    public EventStreetNumberMustBeProvidedInOrderToAdditionalInfoRule(string? streetNumber, string? additionalInfo)
    {
        _streetNumber = streetNumber;
        _additionalInfo = additionalInfo;
    }

    public bool IsBroken() => _streetNumber.IsNullOrWhiteSpace() && _additionalInfo.IsNotNullOrWhiteSpace();

    public string Message => "A street number must be provided in order to add additional info to the address.";
}