using ChangeMe.Modules.Events.Application.Contracts;

namespace ChangeMe.Modules.Events.Application.Series.DeleteSeries;

public record DeleteSeriesCommand(Guid SeriesId) : CommandBase;