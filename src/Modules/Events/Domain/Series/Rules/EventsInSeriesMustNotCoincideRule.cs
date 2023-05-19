using System.Collections.Immutable;
using ChangeMe.Modules.Events.Domain.Series.ValueObjects.SeriesEventDate;
using ChangeMe.Shared.Domain;
using ChangeMe.Shared.Extensions;

namespace ChangeMe.Modules.Events.Domain.Series.Rules;

public class EventsInSeriesMustNotCoincideRule : IBusinessRule
{
    private readonly IReadOnlyCollection<SeriesEventDate> _eventDates;
    private readonly IReadOnlyCollection<SeriesEventDate> _newEventDates;

    public EventsInSeriesMustNotCoincideRule(IEnumerable<SeriesEventDate> newEventDates)
    {
        _eventDates = ImmutableList<SeriesEventDate>.Empty;
        _newEventDates = newEventDates.ToReadOnly();
    }

    public EventsInSeriesMustNotCoincideRule(IEnumerable<SeriesEventDate> eventDates, SeriesEventDate newEventDate)
    {
        _eventDates = eventDates.ToReadOnly();
        _newEventDates = new List<SeriesEventDate> { newEventDate };
    }

    public EventsInSeriesMustNotCoincideRule(
        IEnumerable<SeriesEventDate> eventDates,
        IEnumerable<SeriesEventDate> newEventDates)
    {
        _eventDates = eventDates.ToReadOnly();
        _newEventDates = newEventDates.ToReadOnly();
    }

    //TODO: Hard unit test this method (generated by ChatGPT)
    public bool IsBroken()
    {
        foreach (var newEventDate in _newEventDates)
        {
            if (_eventDates.Any(
                    oldEventDate =>
                        newEventDate.StartDate < oldEventDate.EndDate &&
                        newEventDate.EndDate > oldEventDate.StartDate))
                return true;

            if (_newEventDates.ExceptOne(newEventDate).Any(
                    otherNewEventDate =>
                        newEventDate.StartDate < otherNewEventDate.EndDate &&
                        newEventDate.EndDate > otherNewEventDate.StartDate))
                return true;
        }

        return false;
    }

    public string Message => "Events in series must not coincide in time.";
}