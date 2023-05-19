namespace ChangeMe.Modules.Events.Application.SingleEvent.GetSingleEvent;

public record SingleEventDto(
    Guid Id,
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
    bool IsForFree,
    string Status,
    string[] Editors);