using ChangeMe.Shared.Domain;

namespace ChangeMe.Modules.Events.Domain.Series.ValueObjects.EventDate.Rules;

public class SeriesEventCannotLastMoreThanOneDayRule : IBusinessRule
{
    private readonly DateTimeOffset _startDate;
    private readonly DateTimeOffset _endDate;

    public SeriesEventCannotLastMoreThanOneDayRule(DateTimeOffset startDate, DateTimeOffset endDate)
    {
        _startDate = startDate;
        _endDate = endDate;
    }

    public bool IsBroken() => (_endDate - _startDate).TotalDays > 1;

    public string Message => "The event cannot last more than a day.";
}