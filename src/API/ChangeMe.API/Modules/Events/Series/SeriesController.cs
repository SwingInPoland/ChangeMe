using ChangeMe.API.Modules.Events.Series.Requests.Series;
using ChangeMe.API.Modules.Events.Series.Requests.SeriesEvent;
using ChangeMe.Modules.Events.Application.Contracts;
using ChangeMe.Modules.Events.Application.Series.ChangeSeriesEditors;
using ChangeMe.Modules.Events.Application.Series.ChangeSeriesMainAttributes;
using ChangeMe.Modules.Events.Application.Series.CreateSeries;
using ChangeMe.Modules.Events.Application.Series.DeleteSeries;
using ChangeMe.Modules.Events.Application.Series.SeriesEvent.AddSeriesEvents;
using ChangeMe.Modules.Events.Application.Series.SeriesEvent.ChangeSeriesEventEditors;
using ChangeMe.Modules.Events.Application.Series.SeriesEvent.ChangeSeriesEventsMainAttributes;
using ChangeMe.Modules.Events.Application.Series.SeriesEvent.ChangeSeriesEventStatus;
using ChangeMe.Modules.Events.Application.Series.SeriesEvent.RemoveSeriesEvents;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChangeMe.API.Modules.Events.Series;

[ApiController]
[Route("api/events/series")]
public class SeriesController : ControllerBase
{
    private readonly IEventsModule _eventsModule;

    public SeriesController(IEventsModule eventsModule)
    {
        _eventsModule = eventsModule;
    }

    [HttpPost]
    [Authorize]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateSeries([FromBody] CreateSeriesRequest request)
    {
        var seriesId = await _eventsModule.ExecuteCommandAsync(new CreateSeriesCommand(
            request.SeriesNames,
            request.SeriesDescriptions,
            request.SeriesEditors,
            request.EventNames,
            request.EventDescriptions,
            request.EventDates,
            request.EventHostName,
            request.EventHostUrl,
            request.EventImageUrl,
            request.EventUrl,
            request.EventLocationCoordinatesLatitude,
            request.EventLocationCoordinatesLongitude,
            request.EventLocationCity,
            request.EventLocationProvince,
            request.EventLocationPostalCode,
            request.EventLocationName,
            request.EventLocationStreetName,
            request.EventLocationStreetNumber,
            request.EventLocationStreetAdditionalInfo,
            request.EventIsForFree,
            request.EventStatus,
            request.EventEditors));

        return Ok(seriesId);
    }

    [HttpPut("{seriesId}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> EditSeries(
        [FromRoute] Guid seriesId,
        [FromBody] ChangeSeriesMainAttributesRequest request)
    {
        await _eventsModule.ExecuteCommandAsync(new ChangeSeriesMainAttributesCommand(
            seriesId,
            request.Names,
            request.Descriptions));

        return Ok();
    }

    [HttpPut("{seriesId}/editors")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> ChangeSeriesEditors(
        [FromRoute] Guid seriesId,
        [FromBody] ChangeSeriesEditorsRequest request)
    {
        await _eventsModule.ExecuteCommandAsync(new ChangeSeriesEditorsCommand(
            seriesId,
            request.UserIds));

        return Ok();
    }

    [HttpDelete("{seriesId}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteSeries([FromRoute] Guid seriesId)
    {
        await _eventsModule.ExecuteCommandAsync(new DeleteSeriesCommand(seriesId));
        return Ok();
    }

    [HttpPost("{seriesId}/series-events")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> AddSeriesEvents(
        [FromRoute] Guid seriesId,
        [FromBody] AddSeriesEventsRequest request)
    {
        await _eventsModule.ExecuteCommandAsync(new AddSeriesEventsCommand(
            seriesId,
            request.Names,
            request.Descriptions,
            request.Dates,
            request.HostName,
            request.HostUrl,
            request.ImageUrl,
            request.EventUrl,
            request.LocationCoordinatesLatitude,
            request.LocationCoordinatesLongitude,
            request.LocationCity,
            request.LocationProvince,
            request.LocationPostalCode,
            request.LocationName,
            request.LocationStreetName,
            request.LocationStreetNumber,
            request.LocationStreetAdditionalInfo,
            request.IsForFree,
            request.Editors));

        return Ok();
    }

    [HttpDelete("{seriesId}/series-events")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> RemoveSeriesEvents(
        [FromRoute] Guid seriesId,
        [FromBody] RemoveSeriesEventsRequest request)
    {
        await _eventsModule.ExecuteCommandAsync(new RemoveSeriesEventsCommand(
            seriesId,
            request.SeriesEventIds));

        return Ok();
    }

    [HttpPut("{seriesId}/series-events")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> EditSeriesEvents(
        [FromRoute] Guid seriesId,
        [FromBody] ChangeSeriesEventsMainAttributesRequest request)
    {
        await _eventsModule.ExecuteCommandAsync(new ChangeSeriesEventsMainAttributesCommand(
            seriesId,
            request.Names,
            request.Descriptions,
            request.EventData,
            request.HostName,
            request.HostUrl,
            request.ImageUrl,
            request.EventUrl,
            request.LocationCoordinatesLatitude,
            request.LocationCoordinatesLongitude,
            request.LocationCity,
            request.LocationProvince,
            request.LocationPostalCode,
            request.LocationName,
            request.LocationStreetName,
            request.LocationStreetNumber,
            request.LocationStreetAdditionalInfo,
            request.IsForFree));

        return Ok();
    }

    [HttpPut("{seriesId}/series-events/{seriesEventId}/status")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> ChangeSeriesEventStatus(
        [FromRoute] Guid seriesId,
        [FromRoute] Guid seriesEventId,
        [FromBody] ChangeSeriesEventStatusRequest request)
    {
        await _eventsModule.ExecuteCommandAsync(new ChangeSeriesEventStatusCommand(
            seriesId,
            seriesEventId,
            request.Status));

        return Ok();
    }

    [HttpPut("{seriesId}/series-events/{seriesEventId}/editors")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> ChangeSeriesEventEditors(
        [FromRoute] Guid seriesId,
        [FromRoute] Guid seriesEventId,
        [FromBody] ChangeSeriesEventEditorsRequest request)
    {
        await _eventsModule.ExecuteCommandAsync(new ChangeSeriesEventEditorsCommand(
            seriesId,
            seriesEventId,
            request.UserIds));

        return Ok();
    }
}