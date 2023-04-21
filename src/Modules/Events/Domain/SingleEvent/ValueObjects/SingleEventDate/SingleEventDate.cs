using ChangeMe.Modules.Events.Domain.SingleEvent.ValueObjects.SingleEventDate.Rules;
using ChangeMe.Modules.Events.Domain.ValueObjects.EventDateBase;

namespace ChangeMe.Modules.Events.Domain.SingleEvent.ValueObjects.SingleEventDate;

public class SingleEventDate : EventDateBase
{
    private SingleEventDate(DateTimeOffset startDate, DateTimeOffset endDate) : base(startDate, endDate) { }

    public static SingleEventDate Create(DateTimeOffset startDate, DateTimeOffset endDate)
    {
        CheckRule(new EventCannotLastMoreThanWeekRule(startDate, endDate));
        CheckRule(new EventCannotBeCreated1YearInAdvanceRule(startDate));

        return new SingleEventDate(startDate, endDate);
    }
}