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

    private Series(
        string creatorId,
        SeriesNames names,
        SeriesDescriptions descriptions,
        List<SeriesEvent> seriesEvents,
        HashSet<string> editors)
    {
        Id = new SeriesId(Guid.NewGuid());
        _creatorId = creatorId;
        _names = names;
        _descriptions = descriptions;
        _seriesEvents = seriesEvents;
        _editors = editors;
        _editors.Add(creatorId);

        AddDomainEvent(new SeriesCreatedDomainEvent(Id));
    }

    public static Series Create(
        string creatorId,
        SeriesNames names,
        SeriesDescriptions descriptions,
        List<SeriesEvent> seriesEvents,
        HashSet<string> editors) =>
        new(creatorId, names, descriptions, seriesEvents, editors);

    public void ChangeSeriesMainAttributes(
        string userId,
        SeriesNames names,
        SeriesDescriptions descriptions)
    {
        CheckRule(new UserHasToBeInEditorsRule(_editors, userId));
        _names = names;
        _descriptions = descriptions;

        AddDomainEvent(new SeriesMainAttributesChangedDomainEvent(Id));
    }

    public void AddUserToEditors(string userId, string newUserId)
    {
        CheckRule(new UserHasToBeInEditorsRule(_editors, userId));
        CheckRule(new CannotAddUserToEditorsTwiceRule(_editors, newUserId));
        CheckRule(new EditorsNumberMayNotExceed20Rule(_editors));
        _editors.Add(newUserId);

        AddDomainEvent(new UserAddedToSeriesEditorsDomainEvent(Id));
    }

    public void RemoveUserFromEditors(string userId, string oldUserId)
    {
        CheckRule(new UserHasToBeInEditorsRule(_editors, userId));
        CheckRule(new EditorMustExistRule(_editors, oldUserId));
        CheckRule(new CannotRemoveCreatorFromEditorsRule(_creatorId, oldUserId));
        _editors.Remove(oldUserId);

        AddDomainEvent(new UserRemovedFromSeriesEditorsDomainEvent(Id));
    }

    public void AddSeriesEvent(
        string userId,
        EventNames names,
        EventDescriptions descriptions,
        SeriesEventDate date,
        EventHost host,
        EventImage image,
        EventUrl url,
        EventLocation location,
        bool isForFree,
        HashSet<string> editors)
    {
        CheckRule(new UserHasToBeInEditorsRule(_editors, userId));
        CheckRule(new EventsInSeriesMustNotCoincideRule(GetSeriesEventsDates(), date));

        var seriesEvent = SeriesEvent.Create(userId, Id, names, descriptions, date, host, image, url, location,
            isForFree, editors);
        _seriesEvents.Add(seriesEvent);

        AddDomainEvent(new SeriesEventAddedDomainEvent(Id, seriesEvent.Id));
    }

    public void AddManySeriesEvents(
        string userId,
        EventNames names,
        EventDescriptions descriptions,
        IReadOnlyCollection<SeriesEventDate> dates,
        EventHost host,
        EventImage image,
        EventUrl url,
        EventLocation location,
        bool isForFree,
        HashSet<string> editors)
    {
        CheckRule(new UserHasToBeInEditorsRule(_editors, userId));
        CheckRule(new EventsInSeriesMustNotCoincideRule(GetSeriesEventsDates(), dates));

        var seriesEvents = dates.Select(date =>
            SeriesEvent.Create(userId, Id, names, descriptions, date, host, image, url, location, isForFree,
                editors)).ToReadOnly();

        _seriesEvents.AddRange(seriesEvents);

        AddDomainEvent(new ManySeriesEventsAddedDomainEvent(Id, seriesEvents.Select(x => x.Id).ToReadOnly()));
    }

    public void RemoveSeriesEvent(string userId, SeriesEventId seriesEventId)
    {
        CheckRule(new SeriesEventMustExistsRule(_seriesEvents, seriesEventId));

        var seriesEvent = GetEventById(seriesEventId);
        CheckRule(new UserHasToBeInSeriesOrEventEditorsRule(_editors, seriesEvent.Editors, userId));

        _seriesEvents.Remove(seriesEvent);
        seriesEvent.Delete(userId);

        AddDomainEvent(new SeriesEventRemovedDomainEvent(Id, seriesEventId));
    }

    public void RemoveManySeriesEvents(string userId, IReadOnlyCollection<SeriesEventId> seriesEventIds)
    {
        CheckRule(new SeriesEventMustExistsRule(_seriesEvents, seriesEventIds));

        foreach (var id in seriesEventIds)
        {
            var seriesEvent = GetEventById(id);
            CheckRule(new UserHasToBeInSeriesOrEventEditorsRule(_editors, seriesEvent.Editors, userId));

            _seriesEvents.Remove(seriesEvent);
            seriesEvent.Delete(userId);
        }

        AddDomainEvent(new ManySeriesEventsRemovedDomainEvent(Id, seriesEventIds));
    }

    public void ChangeSeriesEventMainAttributes(
        string userId,
        SeriesEventId seriesEventId,
        EventNames names,
        EventDescriptions descriptions,
        SeriesEventDate date,
        EventHost host,
        EventImage image,
        EventUrl url,
        EventLocation location,
        bool isForFree)
    {
        CheckRule(new SeriesEventMustExistsRule(_seriesEvents, seriesEventId));
        CheckRule(new EventsInSeriesMustNotCoincideRule(GetSeriesEventsDates(date), date));

        var seriesEvent = GetEventById(seriesEventId);
        CheckRule(new UserHasToBeInSeriesOrEventEditorsRule(_editors, seriesEvent.Editors, userId));

        seriesEvent.ChangeMainAttributes(userId, names, descriptions, date, host, image, url, location, isForFree);

        AddDomainEvent(new SeriesEventMainAttributesChangedDomainEvent(seriesEventId));
    }

    public void ChangeManySeriesEventsMainAttributes(
        string userId,
        IReadOnlyCollection<(SeriesEventId seriesEventId, SeriesEventDate date)> eventsData,
        EventNames names,
        EventDescriptions descriptions,
        EventHost host,
        EventImage image,
        EventUrl url,
        EventLocation location,
        bool isForFree)
    {
        CheckRule(new SeriesEventMustExistsRule(_seriesEvents, eventsData.Select(e => e.seriesEventId).ToReadOnly()));

        foreach (var (seriesEventId, date) in eventsData)
        {
            CheckRule(new EventsInSeriesMustNotCoincideRule(GetSeriesEventsDates(date), date));

            var seriesEvent = GetEventById(seriesEventId);
            CheckRule(new UserHasToBeInSeriesOrEventEditorsRule(_editors, seriesEvent.Editors, userId));

            seriesEvent.ChangeMainAttributes(userId, names, descriptions, date, host, image, url, location, isForFree);
        }

        AddDomainEvent(new ManySeriesEventsMainAttributesChangedDomainEvent(Id,
            eventsData.Select(e => e.seriesEventId).ToReadOnly()));
    }

    public void ChangeSeriesEventStatus(string userId, SeriesEventId seriesEventId, string status)
    {
        CheckRule(new SeriesEventMustExistsRule(_seriesEvents, seriesEventId));

        var seriesEvent = GetEventById(seriesEventId);
        CheckRule(new UserHasToBeInSeriesOrEventEditorsRule(_editors, seriesEvent.Editors, userId));

        seriesEvent.ChangeStatus(userId, status);

        AddDomainEvent(new SeriesEventStatusChangedDomainEvent(seriesEventId));
    }

    public void AddUserToSeriesEventEditors(string userId, SeriesEventId seriesEventId, string newUserId)
    {
        CheckRule(new SeriesEventMustExistsRule(_seriesEvents, seriesEventId));
        CheckRule(new CannotAddUserToEventEditorsIfIsInSeriesEditorsRule(_editors, newUserId));

        var seriesEvent = GetEventById(seriesEventId);
        CheckRule(new UserHasToBeInSeriesOrEventEditorsRule(_editors, seriesEvent.Editors, userId));

        seriesEvent.AddUserToEditors(userId, newUserId);

        AddDomainEvent(new UserAddedToSeriesEventEditorsDomainEvent(seriesEventId));
    }

    public void RemoveUserFromSeriesEventEditors(string userId, SeriesEventId seriesEventId, string oldUserId)
    {
        CheckRule(new SeriesEventMustExistsRule(_seriesEvents, seriesEventId));

        var seriesEvent = GetEventById(seriesEventId);
        CheckRule(new UserHasToBeInSeriesOrEventEditorsRule(_editors, seriesEvent.Editors, userId));

        seriesEvent.RemoveUserFromEditors(userId, oldUserId);

        AddDomainEvent(new UserRemovedFromSeriesEventEditorsDomainEvent(seriesEventId));
    }

    // How is it deleted?
    public void Delete(string userId)
    {
        CheckRule(new UserHasToBeInEditorsRule(_editors, userId));
        CheckRule(new AnySeriesEventCannotBeAfterEndRule(GetSeriesEventsEndDates()));

        foreach (var seriesEvent in _seriesEvents)
            seriesEvent.Delete(userId);

        AddDomainEvent(new SeriesDeletedDomainEvent(Id));
    }

    private SeriesEvent GetEventById(SeriesEventId id) => _seriesEvents.Single(x => x.Id == id);

    private IEnumerable<DateTimeOffset> GetSeriesEventsEndDates() => _seriesEvents.Select(e => e.Date.EndDate);

    private IEnumerable<SeriesEventDate> GetSeriesEventsDates(SeriesEventDate? except = null) =>
        _seriesEvents.Select(e => e.Date).ExceptOne(except);
}