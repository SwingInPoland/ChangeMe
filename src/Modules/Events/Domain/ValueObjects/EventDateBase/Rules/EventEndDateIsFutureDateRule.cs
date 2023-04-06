using ChangeMe.Shared.Domain;

namespace ChangeMe.Modules.Events.Domain.ValueObjects.EventDateBase.Rules;

public class EventEndDateIsFutureDateRule : IBusinessRule
{
    private readonly DateTimeOffset _endDate;

    public EventEndDateIsFutureDateRule(DateTimeOffset endDate)
    {
        _endDate = endDate;
    }

    public bool IsBroken() => _endDate < SystemClock.UtcNow;

    public string Message => "The end date of the event must be a future date.";
}