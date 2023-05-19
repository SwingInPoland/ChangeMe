using ChangeMe.Modules.Events.Domain.Rules;
using ChangeMe.Modules.Events.Domain.Series.ValueObjects.SeriesEventDate;
using ChangeMe.Modules.Events.Domain.Series.ValueObjects.SeriesEventStatus;
using ChangeMe.Modules.Events.Domain.ValueObjects.EventDescriptions;
using ChangeMe.Modules.Events.Domain.ValueObjects.EventHost;
using ChangeMe.Modules.Events.Domain.ValueObjects.EventImage;
using ChangeMe.Modules.Events.Domain.ValueObjects.EventLocation;
using ChangeMe.Modules.Events.Domain.ValueObjects.EventNames;
using ChangeMe.Modules.Events.Domain.ValueObjects.EventUrl;
using ChangeMe.Shared.Domain;

namespace ChangeMe.Modules.Events.Domain.Series.Entities.SeriesEvent;

public class SeriesEvent : Entity
{
    internal SeriesEventId Id { get; }
    private SeriesId _seriesId;
    private EventNames _names;
    private EventDescriptions _descriptions;
    internal SeriesEventDate Date { get; private set; }
    private EventHost _host;
    private EventImage _image;
    private EventUrl _url;
    private EventLocation _location;
    private bool _isForFree;
    private SeriesEventStatus _status;
    internal HashSet<string> Editors { get; private set; }
    private string _creatorId;

    /// <summary>
    /// EF Core
    /// </summary>
    private SeriesEvent() { }

    private SeriesEvent(
        string creatorId,
        SeriesId seriesId,
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
        Id = new SeriesEventId(Guid.NewGuid());
        _creatorId = creatorId;
        _seriesId = seriesId;
        _names = names;
        _descriptions = descriptions;
        Date = date;
        _host = host;
        _image = image;
        _url = url;
        _location = location;
        _isForFree = isForFree;
        _status = SeriesEventStatus.Create();
        Editors = editors;
        Editors.Add(creatorId);
    }

    internal static SeriesEvent Create(
        string creatorId,
        SeriesId seriesId,
        EventNames names,
        EventDescriptions descriptions,
        SeriesEventDate date,
        EventHost host,
        EventImage image,
        EventUrl url,
        EventLocation location,
        bool isForFree,
        HashSet<string> editors) =>
        new(creatorId, seriesId, names, descriptions, date, host, image, url, location, isForFree, editors);

    internal void ChangeMainAttributes(
        string userId,
        EventNames names,
        EventDescriptions descriptions,
        SeriesEventDate date,
        EventHost host,
        EventImage image,
        EventUrl url,
        EventLocation location,
        bool isForFree)
    {
        CheckRule(new EventCannotBeChangedAfterEndRule(Date.EndDate));
        _names = names;
        _descriptions = descriptions;
        Date = date;
        _host = host;
        _image = image;
        _url = url;
        _location = location;
        _isForFree = isForFree;
    }

    internal void ChangeStatus(string status)
    {
        CheckRule(new EventCannotBeChangedAfterEndRule(Date.EndDate));
        _status = _status.ChangeStatus(status);
    }

    internal void ChangeEditors(HashSet<string> userIds)
    {
        CheckRule(new EventCannotBeChangedAfterEndRule(Date.EndDate));
        CheckRule(new EditorsNumberMayNotExceed20Rule(userIds));
        CheckRule(new CreatorMustBeInEditorsRule(_creatorId, userIds));

        Editors = userIds;
    }

    // How is it deleted?
    internal void Delete()
    {
        CheckRule(new EventCannotBeChangedAfterEndRule(Date.EndDate));
    }
}