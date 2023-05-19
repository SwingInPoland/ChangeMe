using ChangeMe.Modules.Events.Application.Contracts;

namespace ChangeMe.Modules.Events.Application.Series.ChangeSeriesEditors;

public record ChangeSeriesEditorsCommand(Guid SeriesId, string[] UserIds) : CommandBase;