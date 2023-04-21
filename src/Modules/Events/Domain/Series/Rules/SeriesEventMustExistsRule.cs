using ChangeMe.Modules.Events.Domain.Series.Entities.SeriesEvent;
using ChangeMe.Shared.Domain;

namespace ChangeMe.Modules.Events.Domain.Series.Rules;

public class SeriesEventMustExistsRule : IBusinessRule
{
    private readonly IReadOnlyCollection<SeriesEvent> _events;
    private readonly IReadOnlyCollection<SeriesEventId> _ids;

    public SeriesEventMustExistsRule(IReadOnlyCollection<SeriesEvent> events, SeriesEventId id)
    {
        _events = events;
        _ids = new List<SeriesEventId> { id };
    }

    public SeriesEventMustExistsRule(IReadOnlyCollection<SeriesEvent> events, IReadOnlyCollection<SeriesEventId> ids)
    {
        _events = events;
        _ids = ids;
    }

    public bool IsBroken() => _ids.Any(id => _events.SingleOrDefault(e => e.Id == id) == default);

    public string Message => "Series event must exists.";
}