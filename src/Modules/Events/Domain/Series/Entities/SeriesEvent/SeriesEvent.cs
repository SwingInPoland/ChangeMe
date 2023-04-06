using ChangeMe.Modules.Events.Domain.Events.Rules;
using ChangeMe.Modules.Events.Domain.Series.DomainEvents;
using ChangeMe.Modules.Events.Domain.Series.ValueObjects.EventDate;
using ChangeMe.Modules.Events.Domain.ValueObjects.EventDateBase;
using ChangeMe.Modules.Events.Domain.ValueObjects.EventNameBase;

namespace ChangeMe.Modules.Events.Domain.Series.Entities.SeriesEvent;

public class SeriesEvent : EventBase
{
    private SeriesEvent(List<EventNameBase> names, EventDateBase date) : base(names, date)
    {
        AddDomainEvent(new SeriesEventCreatedDomainEvent(EventId));
    }
    
    public static SeriesEvent Create(List<EventNameBase> names, SeriesEventDate date)
    {
        CheckRule(new EventMustHavePolishNameRule(names));
        return new SeriesEvent(names, date);
    }
}