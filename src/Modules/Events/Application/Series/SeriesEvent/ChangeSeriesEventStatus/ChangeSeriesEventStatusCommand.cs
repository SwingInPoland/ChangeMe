using ChangeMe.Modules.Events.Application.Contracts;

namespace ChangeMe.Modules.Events.Application.Series.SeriesEvent.ChangeSeriesEventStatus;

public record ChangeSeriesEventStatusCommand(Guid SeriesId, Guid SeriesEventId, string Status) : CommandBase;