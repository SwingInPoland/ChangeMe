using ChangeMe.Modules.Events.Application.Contracts;

namespace ChangeMe.Modules.Events.Application.Series.CreateSeries;

public record CreateSeriesCommand(
    IDictionary<string, string> SeriesNames,
    IDictionary<string, string> SeriesDescriptions,
    string[] SeriesEditors,
    IDictionary<string, string> EventNames,
    IDictionary<string, string> EventDescriptions,
    (long StartDate, long EndDate)[] EventDates,
    string EventHostName,
    string EventHostUrl,
    string EventImageUrl,
    string EventUrl,
    float EventLocationCoordinatesLatitude,
    float EventLocationCoordinatesLongitude,
    string EventLocationCity,
    string EventLocationProvince,
    string EventLocationPostalCode,
    string? EventLocationName,
    string? EventLocationStreetName,
    string? EventLocationStreetNumber,
    string? EventLocationStreetAdditionalInfo,
    bool EventIsForFree,
    string EventStatus,
    string[] EventEditors) : CommandBase<Guid>;