using ChangeMe.Shared.Domain;
using ChangeMe.Shared.Extensions;

namespace ChangeMe.Modules.Events.Domain.Series.Rules;

public class AnySeriesEventCannotBeAfterEndRule : IBusinessRule
{
    private readonly IReadOnlyCollection<DateTimeOffset> _endDates;

    public AnySeriesEventCannotBeAfterEndRule(IEnumerable<DateTimeOffset> endDates)
    {
        _endDates = endDates.ToReadOnly();
    }

    public bool IsBroken() => _endDates.Any(endDate => endDate < SystemClock.UtcNow);

    public string Message => "One or more events in the series have already ended.";
}