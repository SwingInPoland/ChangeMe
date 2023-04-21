using ChangeMe.Shared.Domain;
using ChangeMe.Shared.Extensions;

namespace ChangeMe.Modules.Events.Domain.ValueObjects.EventLocation.Rules;

public class EventStreetNumberMustNotExceed10CharactersRule : IBusinessRule
{
    private readonly string? _streetNumber;

    public EventStreetNumberMustNotExceed10CharactersRule(string? streetNumber)
    {
        _streetNumber = streetNumber;
    }

    public bool IsBroken() => _streetNumber.IsNotNullOrWhiteSpace() && _streetNumber!.Length > 10;

    public string Message => "Event street number must not exceed 10 characters.";
}