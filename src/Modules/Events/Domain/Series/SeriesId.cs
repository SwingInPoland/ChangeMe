using ChangeMe.Shared.Domain;

namespace ChangeMe.Modules.Events.Domain.Series;

public class SeriesId : TypedIdValueBase
{
    public SeriesId(Guid value) : base(value) { }
}