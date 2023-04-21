using ChangeMe.Modules.Events.Domain.ValueObjects.EventStatusBase;

namespace ChangeMe.Modules.Events.Domain.Series.ValueObjects.SeriesEventStatus;

public class SeriesEventStatus : EventStatusBase
{
    private SeriesEventStatus(string value = EventStatuses.Planning) : base(value) { }

    public static SeriesEventStatus Create() => new();

    internal override SeriesEventStatus ChangeStatus(string value) => new(value);
}