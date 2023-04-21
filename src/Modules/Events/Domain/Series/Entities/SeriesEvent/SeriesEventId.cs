using ChangeMe.Shared.Domain;

namespace ChangeMe.Modules.Events.Domain.Series.Entities.SeriesEvent;

public class SeriesEventId : TypedIdValueBase
{
    public SeriesEventId(Guid value) : base(value) { }
}