namespace ChangeMe.API.Modules.Events.SingleEvent.Requests;

public record ChangeSingleEventMainAttributesRequest(
    IDictionary<string, string> Names,
    IDictionary<string, string> Descriptions,
    long StartDate,
    long EndDate,
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
    bool IsForFree);