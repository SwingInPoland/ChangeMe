using ChangeMe.Modules.Events.Domain.Rules;
using ChangeMe.Modules.Events.Domain.SingleEvent.DomainEvents;
using ChangeMe.Modules.Events.Domain.SingleEvent.ValueObjects.SingleEventDate;
using ChangeMe.Modules.Events.Domain.SingleEvent.ValueObjects.SingleEventStatus;
using ChangeMe.Modules.Events.Domain.ValueObjects.EventDescriptions;
using ChangeMe.Modules.Events.Domain.ValueObjects.EventHost;
using ChangeMe.Modules.Events.Domain.ValueObjects.EventImage;
using ChangeMe.Modules.Events.Domain.ValueObjects.EventLocation;
using ChangeMe.Modules.Events.Domain.ValueObjects.EventNames;
using ChangeMe.Modules.Events.Domain.ValueObjects.EventUrl;
using ChangeMe.Shared.Domain;

namespace ChangeMe.Modules.Events.Domain.SingleEvent;

//TODO: Make classes and records sealed. Maybe write some ArchTests for that?
public class SingleEvent : Entity, IAggregateRoot
{
    public SingleEventId Id { get; }
    private EventNames _names;
    private EventDescriptions _descriptions;
    private SingleEventDate _date;
    private EventHost _host;
    private EventImage _image;
    private EventUrl _url;
    private EventLocation _location;
    private bool _isForFree;
    private SingleEventStatus _status;
    private HashSet<string> _editors;
    private string _creatorId;

    /// <summary>
    /// EF Core
    /// </summary>
    private SingleEvent() { }

    private SingleEvent(
        string creatorId,
        EventNames names,
        EventDescriptions descriptions,
        SingleEventDate date,
        EventHost host,
        EventImage image,
        EventUrl url,
        EventLocation location,
        bool isForFree,
        SingleEventStatus status,
        HashSet<string> editors)
    {
        Id = new SingleEventId(Guid.NewGuid());
        _creatorId = creatorId;
        _names = names;
        _descriptions = descriptions;
        _date = date;
        _host = host;
        _image = image;
        _url = url;
        _location = location;
        _isForFree = isForFree;
        _status = status;
        _editors = editors;
        _editors.Add(creatorId);

        AddDomainEvent(new SingleEventCreatedDomainEvent(Id));
    }

    public static SingleEvent Create(
        string creatorId,
        EventNames names,
        EventDescriptions descriptions,
        SingleEventDate date,
        EventHost host,
        EventImage image,
        EventUrl url,
        EventLocation location,
        bool isForFree,
        SingleEventStatus status,
        HashSet<string> editors) =>
        new(creatorId, names, descriptions, date, host, image, url, location, isForFree, status, editors);

    public void ChangeMainAttributes(
        string userId,
        EventNames names,
        EventDescriptions descriptions,
        SingleEventDate date,
        EventHost host,
        EventImage image,
        EventUrl url,
        EventLocation location,
        bool isForFree)
    {
        CheckRule(new EventCannotBeChangedAfterEndRule(_date.EndDate));
        CheckRule(new UserHasToBeInEditorsRule(_editors, userId));
        _names = names;
        _descriptions = descriptions;
        _date = date;
        _host = host;
        _image = image;
        _url = url;
        _location = location;
        _isForFree = isForFree;

        AddDomainEvent(new SingleEventMainAttributesChangedDomainEvent(Id));
    }

    public void ChangeStatus(string userId, string status)
    {
        CheckRule(new EventCannotBeChangedAfterEndRule(_date.EndDate));
        CheckRule(new UserHasToBeInEditorsRule(_editors, userId));
        _status = _status.ChangeStatus(status);

        AddDomainEvent(new SingleEventStatusChangedDomainEvent(Id));
    }

    public void ChangeEditors(string userId, HashSet<string> userIds)
    {
        CheckRule(new EventCannotBeChangedAfterEndRule(_date.EndDate));
        CheckRule(new UserHasToBeInEditorsRule(_editors, userId));
        CheckRule(new EditorsNumberMayNotExceed20Rule(userIds));
        CheckRule(new CreatorMustBeInEditorsRule(_creatorId, userIds));

        _editors = userIds;

        AddDomainEvent(new SingleEventEditorsChangedDomainEvent(Id));
    }

    // How is it deleted?
    public void Delete(string userId)
    {
        CheckRule(new EventCannotBeChangedAfterEndRule(_date.EndDate));
        CheckRule(new UserHasToBeInEditorsRule(_editors, userId));

        AddDomainEvent(new SingleEventDeletedDomainEvent(Id));
    }
}