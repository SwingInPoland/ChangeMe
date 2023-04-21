using ChangeMe.Modules.Events.Domain.ValueObjects.EventLocation.Rules;
using ChangeMe.Shared.Domain;

namespace ChangeMe.Modules.Events.Domain.ValueObjects.EventLocation.ValueObjects;

public class Street : ValueObject
{
    public string StreetName { get; }
    public string? StreetNumber { get; }
    public string? AdditionalInfo { get; }

    private Street(string streetName, string? streetNumber, string? additionalInfo)
    {
        StreetName = streetName;
        StreetNumber = streetNumber;
        AdditionalInfo = additionalInfo;
    }

    public static Street? TryCreate(string? streetName, string? streetNumber, string? additionalInfo)
    {
        if (streetName is null && streetNumber is null && additionalInfo is null)
            return null;

        CheckRule(new EventStreetNameMustBeBetween2And300CharactersRule(streetName));
        CheckRule(new EventStreetNumberMustBeProvidedInOrderToAdditionalInfoRule(streetNumber, additionalInfo));

        if (streetNumber is not null)
            CheckRule(new EventStreetNumberMustNotExceed10CharactersRule(streetNumber));

        if (additionalInfo is not null)
            CheckRule(new EventAdditionalInfoMustNotExceed10CharactersRule(additionalInfo));

        return new Street(streetName!, streetNumber, additionalInfo);
    }
}