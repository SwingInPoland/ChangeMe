using ChangeMe.Shared.Domain;

namespace ChangeMe.Modules.Events.Domain.ValueObjects.EventLocation.Rules;

public class EventLatitudeMustBeValidRule : IBusinessRule
{
    private readonly float _latitude;

    public EventLatitudeMustBeValidRule(float latitude)
    {
        _latitude = latitude;
    }

    public bool IsBroken() => _latitude is < -90 or > 90;

    public string Message => "Event latitude must be a valid value between -90 and 90 degrees.";
}