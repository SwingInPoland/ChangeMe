using ChangeMe.Shared.Domain;

namespace ChangeMe.Modules.Events.Domain.ValueObjects.EventDateBase.Rules;

public class EventCannotLastLessThanOneHourRule : IBusinessRule
{
    private readonly DateTimeOffset _startDate;
    private readonly DateTimeOffset _endDate;

    public EventCannotLastLessThanOneHourRule(DateTimeOffset startDate, DateTimeOffset endDate)
    {
        _startDate = startDate;
        _endDate = endDate;
    }

    public bool IsBroken() => (_endDate - _startDate).TotalHours < 1;

    public string Message => "The event cannot last less than one hour.";
}