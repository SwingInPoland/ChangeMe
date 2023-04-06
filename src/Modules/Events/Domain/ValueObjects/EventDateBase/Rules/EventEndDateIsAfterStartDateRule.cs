using ChangeMe.Shared.Domain;

namespace ChangeMe.Modules.Events.Domain.ValueObjects.EventDateBase.Rules;

public class EventEndDateIsAfterStartDateRule : IBusinessRule
{
    private readonly DateTimeOffset _startDate;
    private readonly DateTimeOffset _endDate;

    public EventEndDateIsAfterStartDateRule(DateTimeOffset startDate, DateTimeOffset endDate)
    {
        _startDate = startDate;
        _endDate = endDate;
    }

    public bool IsBroken() => _endDate <= _startDate;

    public string Message => "The end date of the event must be after the start date.";
}