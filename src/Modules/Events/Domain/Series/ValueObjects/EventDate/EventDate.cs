using ChangeMe.Modules.Events.Domain.Series.ValueObjects.EventDate.Rules;
using ChangeMe.Modules.Events.Domain.ValueObjects.EventDateBase;

namespace ChangeMe.Modules.Events.Domain.Series.ValueObjects.EventDate;

public class SeriesEventDate : EventDateBase
{
    private SeriesEventDate(DateTimeOffset startDate, DateTimeOffset endDate) : base(startDate, endDate) { }
    
    public static SeriesEventDate Create(DateTimeOffset startDate, DateTimeOffset endDate)
    {
        CheckRule(new SeriesEventCannotLastMoreThanOneDayRule(startDate, endDate));
        CheckRule(new SeriesEventCannotBeCreated6MonthsInAdvanceRule(startDate));

        return new SeriesEventDate(startDate, endDate);
    }
}