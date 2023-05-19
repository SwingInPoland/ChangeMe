using ChangeMe.Modules.Events.Application.Contracts;

namespace ChangeMe.Modules.Events.Application.Series.ChangeSeriesMainAttributes;

public record ChangeSeriesMainAttributesCommand(
    Guid SeriesId,
    IDictionary<string, string> Names,
    IDictionary<string, string> Descriptions) : CommandBase;