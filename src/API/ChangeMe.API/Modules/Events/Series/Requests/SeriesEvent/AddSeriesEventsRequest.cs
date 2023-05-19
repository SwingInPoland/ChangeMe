namespace ChangeMe.API.Modules.Events.Series.Requests.SeriesEvent;

public record AddSeriesEventsRequest(
    IDictionary<string, string> Names,
    IDictionary<string, string> Descriptions,
    (long StartDate, long EndDate)[] Dates,
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
    bool IsForFree,
    string[] Editors);