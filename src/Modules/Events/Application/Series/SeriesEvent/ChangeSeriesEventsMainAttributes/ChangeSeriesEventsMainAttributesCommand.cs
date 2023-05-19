using ChangeMe.Modules.Events.Application.Contracts;

namespace ChangeMe.Modules.Events.Application.Series.SeriesEvent.ChangeSeriesEventsMainAttributes;

public record ChangeSeriesEventsMainAttributesCommand(
    Guid SeriesId,
    IDictionary<string, string> Names,
    IDictionary<string, string> Descriptions,
    (Guid SeriesEventId, long StartDate, long EndDate)[] EventData,
    string HostName,
    string HostUrl,
    string ImageUrl,
    string EventUrl,
    float LocationCoordinatesLatitude,
    float LocationCoordinatesLongitude,
    string LocationCity,
    string LocationProvince,
    string LocationPostalCode,
    string? LocationName,
    string? LocationStreetName,
    string? LocationStreetNumber,
    string? LocationStreetAdditionalInfo,
    bool IsForFree) : CommandBase;