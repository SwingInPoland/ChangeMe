using ChangeMe.Shared.Domain;

namespace ChangeMe.Modules.Events.Domain.Series.ValueObjects.SeriesEventDate.Rules;

public class SeriesEventCannotBeCreated6MonthsInAdvanceRule : IBusinessRule
{
    private readonly DateTimeOffset _startDate;

    public SeriesEventCannotBeCreated6MonthsInAdvanceRule(DateTimeOffset startDate)
    {
        _startDate = startDate;
    }

    public bool IsBroken() => _startDate > SystemClock.UtcNow.AddMonths(6);

    public string Message => "A series event cannot be created more than 6 months in advance.";
}