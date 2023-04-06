using ChangeMe.Shared.Domain;

namespace ChangeMe.Modules.Events.Domain.ValueObjects.EventDateBase.Rules;

public class EventStartDateIsFutureDateRule : IBusinessRule
{
    private readonly DateTimeOffset _startDate;

    public EventStartDateIsFutureDateRule(DateTimeOffset startDate)
    {
        _startDate = startDate;
    }

    public bool IsBroken() => _startDate < SystemClock.UtcNow;

    public string Message => "The start date of the event must be a future date.";
}