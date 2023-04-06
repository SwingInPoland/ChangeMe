using ChangeMe.Modules.Events.Domain.ValueObjects.EventDateBase.Rules;
using ChangeMe.Shared.Domain;

namespace ChangeMe.Modules.Events.Domain.ValueObjects.EventDateBase;

public abstract class EventDateBase : ValueObject
{
    public DateTimeOffset StartDate { get; }
    public DateTimeOffset EndDate { get; }

    protected EventDateBase(DateTimeOffset startDate, DateTimeOffset endDate)
    {
        CheckRule(new EventStartDateIsFutureDateRule(startDate));
        CheckRule(new EventEndDateIsFutureDateRule(endDate));
        CheckRule(new EventEndDateIsAfterStartDateRule(startDate, endDate));
        CheckRule(new EventCannotLastLessThanOneHourRule(startDate, endDate));
        
        StartDate = startDate;
        EndDate = endDate;
    }
}