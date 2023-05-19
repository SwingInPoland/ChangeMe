using ChangeMe.API.Modules.Events.SingleEvent.Requests;
using ChangeMe.Modules.Events.Application.Contracts;
using ChangeMe.Modules.Events.Application.SingleEvent.ChangeSingleEventEditors;
using ChangeMe.Modules.Events.Application.SingleEvent.ChangeSingleEventMainAttributes;
using ChangeMe.Modules.Events.Application.SingleEvent.ChangeSingleEventStatus;
using ChangeMe.Modules.Events.Application.SingleEvent.CreateSingleEvent;
using ChangeMe.Modules.Events.Application.SingleEvent.DeleteSingleEvent;
using ChangeMe.Modules.Events.Application.SingleEvent.GetSingleEvent;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChangeMe.API.Modules.Events.SingleEvent;

[ApiController]
[Route("api/events/single-events")]
public class SingleEventsController : ControllerBase
{
    private readonly IEventsModule _eventsModule;

    public SingleEventsController(IEventsModule eventsModule)
    {
        _eventsModule = eventsModule;
    }

    [AllowAnonymous]
    [HttpGet("{singleEventId}")]
    [ProducesResponseType(typeof(SingleEventDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetSingleEvent(Guid singleEventId)
    {
        var singleEvent = await _eventsModule.ExecuteQueryAsync(new GetSingleEventQuery(singleEventId));
        return Ok(singleEvent);
    }

    [HttpPost]
    [Authorize]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateSingleEvent([FromBody] CreateSingleEventRequest request)
    {
        var singleEventId = await _eventsModule.ExecuteCommandAsync(new CreateSingleEventCommand(
            request.Names,
            request.Descriptions,
            request.StartDate,
            request.EndDate,
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
            request.Status,
            request.Editors));

        return Ok(singleEventId);
    }

    [HttpPut("{singleEventId}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> EditSingleEvent(
        [FromRoute] Guid singleEventId,
        [FromBody] ChangeSingleEventMainAttributesRequest request)
    {
        await _eventsModule.ExecuteCommandAsync(new ChangeSingleEventMainAttributesCommand(
            singleEventId,
            request.Names,
            request.Descriptions,
            request.StartDate,
            request.EndDate,
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

    [HttpPut("{singleEventId}/status")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> ChangeSingleEventStatus(
        [FromRoute] Guid singleEventId,
        [FromBody] ChangeSingleEventStatusRequest request)
    {
        await _eventsModule.ExecuteCommandAsync(new ChangeSingleEventStatusCommand(
            singleEventId,
            request.Status));

        return Ok();
    }

    [HttpPut("{singleEventId}/editors")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> ChangeSingleEventEditors(
        [FromRoute] Guid singleEventId,
        [FromBody] ChangeSingleEventEditorsRequest request)
    {
        await _eventsModule.ExecuteCommandAsync(new ChangeSingleEventEditorsCommand(
            singleEventId,
            request.UserIds));

        return Ok();
    }

    [HttpDelete("{singleEventId}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteSingleEvent(Guid singleEventId)
    {
        await _eventsModule.ExecuteCommandAsync(new DeleteSingleEventCommand(singleEventId));
        return Ok();
    }
}