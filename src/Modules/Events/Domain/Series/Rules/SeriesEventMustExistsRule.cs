using ChangeMe.Modules.Events.Domain.Series.Entities.SeriesEvent;
using ChangeMe.Shared.Domain;
using ChangeMe.Shared.Extensions;

namespace ChangeMe.Modules.Events.Domain.Series.Rules;

public class SeriesEventMustExistsRule : IBusinessRule
{
    private readonly IReadOnlyCollection<SeriesEventId> _existing;
    private readonly IReadOnlyCollection<SeriesEventId> _toCheck;

    public SeriesEventMustExistsRule(IEnumerable<SeriesEventId> existing, SeriesEventId toCheck)
    {
        _existing = existing.ToReadOnly();
        _toCheck = new List<SeriesEventId> { toCheck };
    }

    public SeriesEventMustExistsRule(IEnumerable<SeriesEventId> existing, IEnumerable<SeriesEventId> toCheck)
    {
        _existing = existing.ToReadOnly();
        _toCheck = toCheck.ToReadOnly();
    }

    public bool IsBroken() => _toCheck.Any(id => _existing.NotContains(id));

    public string Message => "Series event must exists.";
}