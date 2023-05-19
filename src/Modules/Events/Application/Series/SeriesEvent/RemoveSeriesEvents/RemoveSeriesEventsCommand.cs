using ChangeMe.Modules.Events.Application.Contracts;

namespace ChangeMe.Modules.Events.Application.Series.SeriesEvent.RemoveSeriesEvents;

public record RemoveSeriesEventsCommand(Guid SeriesId, Guid[] SeriesEventIds) : CommandBase;