using ChangeMe.Shared.Domain;

namespace ChangeMe.Modules.Events.Domain.SingleEvent.ValueObjects.SingleEventDate.Rules;

public class EventCannotLastMoreThanWeekRule : IBusinessRule
{
    private readonly DateTimeOffset _startDate;
    private readonly DateTimeOffset _endDate;

    public EventCannotLastMoreThanWeekRule(DateTimeOffset startDate, DateTimeOffset endDate)
    {
        _startDate = startDate;
        _endDate = endDate;
    }

    public bool IsBroken() => (_endDate - _startDate).TotalDays > 7;

    public string Message => "The event cannot last more than a week.";
}