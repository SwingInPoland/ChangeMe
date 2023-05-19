using ChangeMe.Modules.Events.Application.Contracts;

namespace ChangeMe.Modules.Events.Application.Series.SeriesEvent.ChangeSeriesEventEditors;

public record ChangeSeriesEventEditorsCommand(Guid SeriesId, Guid SeriesEventId, string[] UserIds) : CommandBase;