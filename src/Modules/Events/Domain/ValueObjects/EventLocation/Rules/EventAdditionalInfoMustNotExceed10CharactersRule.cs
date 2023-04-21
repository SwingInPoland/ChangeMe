using ChangeMe.Shared.Domain;
using ChangeMe.Shared.Extensions;

namespace ChangeMe.Modules.Events.Domain.ValueObjects.EventLocation.Rules;

public class EventAdditionalInfoMustNotExceed10CharactersRule : IBusinessRule
{
    private readonly string? _additionalInfo;

    public EventAdditionalInfoMustNotExceed10CharactersRule(string? additionalInfo)
    {
        _additionalInfo = additionalInfo;
    }

    public bool IsBroken() => _additionalInfo.IsNotNullOrWhiteSpace() && _additionalInfo!.Length > 10;

    public string Message => "Event additional info must not exceed 10 characters.";
}