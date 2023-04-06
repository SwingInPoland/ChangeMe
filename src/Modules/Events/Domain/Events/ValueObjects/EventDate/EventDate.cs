using ChangeMe.Modules.Events.Domain.Events.ValueObjects.EventDate.Rules;
using ChangeMe.Modules.Events.Domain.ValueObjects.EventDateBase;

namespace ChangeMe.Modules.Events.Domain.Events.ValueObjects.EventDate;

public class EventDate : EventDateBase
{
    private EventDate(DateTimeOffset startDate, DateTimeOffset endDate) : base(startDate, endDate) { }
    
    public static EventDate Create(DateTimeOffset startDate, DateTimeOffset endDate)
    {
        CheckRule(new EventCannotLastMoreThanWeekRule(startDate, endDate));
        CheckRule(new EventCannotBeCreated1YearInAdvanceRule(startDate));
        
        return new EventDate(startDate, endDate);
    }
}