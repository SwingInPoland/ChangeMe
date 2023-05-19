namespace ChangeMe.API.Modules.Events.Series.Requests.Series;

public record ChangeSeriesMainAttributesRequest(
    IDictionary<string, string> Names,
    IDictionary<string, string> Descriptions);