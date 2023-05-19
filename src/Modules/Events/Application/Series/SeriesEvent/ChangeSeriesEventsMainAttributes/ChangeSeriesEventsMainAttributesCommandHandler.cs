using ChangeMe.Modules.Events.Application.Configuration.Commands;
using ChangeMe.Modules.Events.Domain.Series;
using ChangeMe.Modules.Events.Domain.Series.Entities.SeriesEvent;
using ChangeMe.Modules.Events.Domain.Series.ValueObjects.SeriesEventDate;
using ChangeMe.Modules.Events.Domain.ValueObjects.EventDescriptions;
using ChangeMe.Modules.Events.Domain.ValueObjects.EventHost;
using ChangeMe.Modules.Events.Domain.ValueObjects.EventImage;
using ChangeMe.Modules.Events.Domain.ValueObjects.EventLocation;
using ChangeMe.Modules.Events.Domain.ValueObjects.EventLocation.ValueObjects;
using ChangeMe.Modules.Events.Domain.ValueObjects.EventNames;
using ChangeMe.Modules.Events.Domain.ValueObjects.EventUrl;
using ChangeMe.Modules.Events.Domain.ValueObjects.TranslationBase;
using ChangeMe.Shared.Application;
using ChangeMe.Shared.Extensions;
using MediatR;

namespace ChangeMe.Modules.Events.Application.Series.SeriesEvent.ChangeSeriesEventsMainAttributes;

internal class ChangeSeriesEventsMainAttributesCommandHandler : ICommandHandler<ChangeSeriesEventsMainAttributesCommand>
{
    private readonly ISeriesRepository _seriesRepository;
    private readonly IExecutionContextAccessor _executionContextAccessor;

    public ChangeSeriesEventsMainAttributesCommandHandler(
        ISeriesRepository seriesRepository,
        IExecutionContextAccessor executionContextAccessor)
    {
        _seriesRepository = seriesRepository;
        _executionContextAccessor = executionContextAccessor;
    }

    public async Task<Unit> Handle(ChangeSeriesEventsMainAttributesCommand request, CancellationToken cancellationToken)
    {
        var series = await _seriesRepository.GetByIdAsync(new SeriesId(request.SeriesId), cancellationToken);

        var namesList = new List<TranslationValueBase>();
        if (request.Names.TryGetValue("pl", out var polishName))
            namesList.Add(PolishEventName.Create(polishName));
        if (request.Names.TryGetValue("en", out var englishName))
            namesList.Add(EnglishEventName.Create(englishName));
        if (request.Names.TryGetValue("uk", out var ukrainianName))
            namesList.Add(UkrainianEventName.Create(ukrainianName));

        var descriptionsList = new List<TranslationValueBase>();
        if (request.Descriptions.TryGetValue("pl", out var polishDescription))
            descriptionsList.Add(PolishEventDescription.Create(polishDescription));
        if (request.Descriptions.TryGetValue("en", out var englishDescription))
            descriptionsList.Add(EnglishEventDescription.Create(englishDescription));
        if (request.Descriptions.TryGetValue("uk", out var ukrainianDescription))
            descriptionsList.Add(UkrainianEventDescription.Create(ukrainianDescription));

        var eventLocation = EventLocation.Create(
            Coordinates.Create(request.LocationCoordinatesLatitude, request.LocationCoordinatesLongitude),
            City.Create(request.LocationCity),
            Province.Create(request.LocationProvince),
            PostalCode.Create(request.LocationPostalCode),
            LocationName.TryCreate(request.LocationName),
            Street.TryCreate(request.LocationStreetName, request.LocationStreetNumber,
                request.LocationStreetAdditionalInfo));

        var eventData = request.EventData.Select(data =>
                (new SeriesEventId(data.SeriesEventId), SeriesEventDate.Create(
                    data.StartDate.ToDateTimeOffset(),
                    data.EndDate.ToDateTimeOffset())))
            .ToList();

        //TODO: How is it updated?
        series.ChangeSeriesEventsMainAttributes(
            _executionContextAccessor.UserId,
            eventData,
            EventNames.Create(namesList),
            EventDescriptions.Create(descriptionsList),
            EventHost.Create(request.HostName, new Uri(request.HostUrl)),
            EventImage.Create(new Uri(request.ImageUrl)),
            EventUrl.Create(new Uri(request.EventUrl)),
            eventLocation,
            request.IsForFree);

        return Unit.Value;
    }
}