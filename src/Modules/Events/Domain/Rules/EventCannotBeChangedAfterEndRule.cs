using ChangeMe.Shared.Domain;

namespace ChangeMe.Modules.Events.Domain.Rules;

public class EventCannotBeChangedAfterEndRule : IBusinessRule
{
    private readonly DateTimeOffset _endDate;

    public EventCannotBeChangedAfterEndRule(DateTimeOffset endDate)
    {
        _endDate = endDate;
    }

    public bool IsBroken() => _endDate < SystemClock.UtcNow;

    public string Message => "Event cannot be changed after it has ended.";
}