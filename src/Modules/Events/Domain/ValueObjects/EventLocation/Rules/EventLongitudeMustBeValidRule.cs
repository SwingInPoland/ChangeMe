using ChangeMe.Shared.Domain;

namespace ChangeMe.Modules.Events.Domain.ValueObjects.EventLocation.Rules;

public class EventLongitudeMustBeValidRule : IBusinessRule
{
    private readonly float _longitude;

    public EventLongitudeMustBeValidRule(float longitude)
    {
        _longitude = longitude;
    }

    public bool IsBroken() => _longitude is < -180 or > 180;

    public string Message => "Event longitude must be a valid value between -180 and 180 degrees.";
}