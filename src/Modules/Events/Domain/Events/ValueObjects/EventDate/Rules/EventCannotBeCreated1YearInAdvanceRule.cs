using ChangeMe.Shared.Domain;

namespace ChangeMe.Modules.Events.Domain.Events.ValueObjects.EventDate.Rules;

public class EventCannotBeCreated1YearInAdvanceRule : IBusinessRule
{
    private readonly DateTimeOffset _startDate;

    public EventCannotBeCreated1YearInAdvanceRule(DateTimeOffset startDate)
    {
        _startDate = startDate;
    }

    public bool IsBroken() => _startDate > SystemClock.UtcNow.AddYears(1);

    public string Message => "An event cannot be created more than 1 year in advance.";
}