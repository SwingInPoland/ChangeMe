using ChangeMe.Modules.Events.Domain.Rules;
using ChangeMe.Modules.Events.Domain.Series.DomainEvents;
using ChangeMe.Modules.Events.Domain.Series.Entities.SeriesEvent;
using ChangeMe.Modules.Events.Domain.Series.Rules;
using ChangeMe.Modules.Events.Domain.Series.ValueObjects.SeriesDescriptions;
using ChangeMe.Modules.Events.Domain.Series.ValueObjects.SeriesEventDate;
using ChangeMe.Modules.Events.Domain.Series.ValueObjects.SeriesNames;
using ChangeMe.Modules.Events.Domain.ValueObjects.EventDescriptions;
using ChangeMe.Modules.Events.Domain.ValueObjects.EventHost;
using ChangeMe.Modules.Events.Domain.ValueObjects.EventImage;
using ChangeMe.Modules.Events.Domain.ValueObjects.EventLocation;
using ChangeMe.Modules.Events.Domain.ValueObjects.EventNames;
using ChangeMe.Modules.Events.Domain.ValueObjects.EventUrl;
using ChangeMe.Shared.Domain;
using ChangeMe.Shared.Extensions;

namespace ChangeMe.Modules.Events.Domain.Series;

public class Series : Entity, IAggregateRoot
{
    public SeriesId Id { get; }
    private SeriesNames _names;
    private SeriesDescriptions _descriptions;
    private List<SeriesEvent> _seriesEvents;
    private HashSet<string> _editors;
    private string _creatorId;

    /// <summary>
    /// EF Core
    /// </summary>
    private Series() { }

    private Series(
        SeriesId? id,
        string userId,
        SeriesNames names,
        SeriesDescriptions descriptions,
        List<SeriesEvent> seriesEvents,
        HashSet<string> editors)
    {
        Id = id ?? new SeriesId(Guid.NewGuid());
        _creatorId = userId;
        _names = names;
        _descriptions = descriptions;
        _seriesEvents = seriesEvents;
        _editors = editors;
        _editors.Add(userId);

        AddDomainEvent(new SeriesCreatedDomainEvent(Id));
    }

    public static Series Create(
        string userId,
        SeriesNames seriesNames,
        SeriesDescriptions seriesDescriptions,
        HashSet<string> seriesEditors,
        EventNames eventNames,
        EventDescriptions eventDescriptions,
        ICollection<SeriesEventDate> eventDates,
        EventHost eventHost,
        EventImage eventImage,
        EventUrl eventUrl,
        EventLocation eventLocation,
        bool eventIsForFree,
        HashSet<string> eventEditors)
    {
        CheckRule(new EditorsNumberMayNotExceed20Rule(seriesEditors));
        CheckRule(new CannotAddUserToEventEditorsIfIsInSeriesEditorsRule(seriesEditors, eventEditors));
        CheckRule(new EventsInSeriesMustNotCoincideRule(eventDates));

        var seriesId = new SeriesId(Guid.NewGuid());
        var seriesEvents = eventDates.Select(date =>
                SeriesEvent.Create(userId, seriesId, eventNames, eventDescriptions, date, eventHost, eventImage,
                    eventUrl, eventLocation, eventIsForFree, eventEditors))
            .ToList();

        return new Series(seriesId, userId, seriesNames, seriesDescriptions, seriesEvents, seriesEditors);
    }

    public void ChangeMainAttributes(
        string userId,
        SeriesNames names,
        SeriesDescriptions descriptions)
    {
        CheckRule(new UserHasToBeInEditorsRule(_editors, userId));
        _names = names;
        _descriptions = descriptions;

        AddDomainEvent(new SeriesMainAttributesChangedDomainEvent(Id));
    }

    public void ChangeEditors(string userId, HashSet<string> userIds)
    {
        CheckRule(new UserHasToBeInEditorsRule(_editors, userId));
        CheckRule(new EditorsNumberMayNotExceed20Rule(userIds));
        CheckRule(new CreatorMustBeInEditorsRule(_creatorId, userIds));
        foreach (var newUserId in userIds)
            _editors.Add(newUserId);

        AddDomainEvent(new SeriesEditorsChangedDomainEvent(Id));
    }

    public void AddSeriesEvents(
        string userId,
        EventNames names,
        EventDescriptions descriptions,
        ICollection<SeriesEventDate> dates,
        EventHost host,
        EventImage image,
        EventUrl url,
        EventLocation location,
        bool isForFree,
        HashSet<string> editors)
    {
        CheckRule(new UserHasToBeInEditorsRule(_editors, userId));
        CheckRule(new EventsInSeriesMustNotCoincideRule(GetSeriesEventsDates(), dates));
        CheckRule(new CannotAddUserToEventEditorsIfIsInSeriesEditorsRule(_editors, editors));

        var seriesEvents = dates.Select(date =>
            SeriesEvent.Create(userId, Id, names, descriptions, date, host, image, url, location, isForFree,
                editors)).ToList();

        _seriesEvents.AddRange(seriesEvents);

        AddDomainEvent(new SeriesEventsAddedDomainEvent(Id, seriesEvents.Select(e => e.Id)));
    }

    public void RemoveSeriesEvents(string userId, HashSet<SeriesEventId> seriesEventIds)
    {
        CheckRule(new SeriesEventMustExistsRule(_seriesEvents.Select(e => e.Id), seriesEventIds));

        foreach (var id in seriesEventIds)
        {
            var seriesEvent = GetEventById(id);
            CheckRule(new UserHasToBeInSeriesOrEventEditorsRule(_editors, seriesEvent.Editors, userId));

            _seriesEvents.Remove(seriesEvent);
            seriesEvent.Delete();
        }

        AddDomainEvent(new SeriesEventsRemovedDomainEvent(Id, seriesEventIds));
    }

    public void ChangeSeriesEventsMainAttributes(
        string userId,
        ICollection<(SeriesEventId seriesEventId, SeriesEventDate date)> eventsData,
        EventNames names,
        EventDescriptions descriptions,
        EventHost host,
        EventImage image,
        EventUrl url,
        EventLocation location,
        bool isForFree)
    {
        var seriesEventsIds = eventsData.Select(e => e.seriesEventId).ToList();

        CheckRule(new SeriesEventMustExistsRule(_seriesEvents.Select(e => e.Id), seriesEventsIds));

        foreach (var (seriesEventId, date) in eventsData)
        {
            CheckRule(new EventsInSeriesMustNotCoincideRule(GetSeriesEventsDates(date), date));

            var seriesEvent = GetEventById(seriesEventId);
            CheckRule(new UserHasToBeInSeriesOrEventEditorsRule(_editors, seriesEvent.Editors, userId));

            seriesEvent.ChangeMainAttributes(userId, names, descriptions, date, host, image, url, location, isForFree);
        }

        AddDomainEvent(new SeriesEventsMainAttributesChangedDomainEvent(Id, seriesEventsIds));
    }

    public void ChangeSeriesEventStatus(string userId, SeriesEventId seriesEventId, string status)
    {
        CheckRule(new SeriesEventMustExistsRule(_seriesEvents.Select(e => e.Id), seriesEventId));

        var seriesEvent = GetEventById(seriesEventId);
        CheckRule(new UserHasToBeInSeriesOrEventEditorsRule(_editors, seriesEvent.Editors, userId));

        seriesEvent.ChangeStatus(status);

        AddDomainEvent(new SeriesEventStatusChangedDomainEvent(seriesEventId));
    }

    public void ChangeSeriesEventEditors(string userId, SeriesEventId seriesEventId, HashSet<string> userIds)
    {
        CheckRule(new SeriesEventMustExistsRule(_seriesEvents.Select(e => e.Id), seriesEventId));
        CheckRule(new CannotAddUserToEventEditorsIfIsInSeriesEditorsRule(_editors, userIds));

        var seriesEvent = GetEventById(seriesEventId);
        CheckRule(new UserHasToBeInSeriesOrEventEditorsRule(_editors, seriesEvent.Editors, userId));

        seriesEvent.ChangeEditors(userIds);

        AddDomainEvent(new SeriesEventEditorsChangedDomainEvent(seriesEventId));
    }

    // How is it deleted?
    public void Delete(string userId)
    {
        CheckRule(new UserHasToBeInEditorsRule(_editors, userId));
        CheckRule(new AnySeriesEventCannotBeAfterEndRule(GetSeriesEventsEndDates()));

        foreach (var seriesEvent in _seriesEvents)
            seriesEvent.Delete();

        AddDomainEvent(new SeriesDeletedDomainEvent(Id));
    }

    private SeriesEvent GetEventById(SeriesEventId id) => _seriesEvents.Single(x => x.Id == id);

    private IEnumerable<DateTimeOffset> GetSeriesEventsEndDates() => _seriesEvents.Select(e => e.Date.EndDate);

    private IEnumerable<SeriesEventDate> GetSeriesEventsDates(SeriesEventDate? except = null) =>
        _seriesEvents.Select(e => e.Date).ExceptOne(except);
}