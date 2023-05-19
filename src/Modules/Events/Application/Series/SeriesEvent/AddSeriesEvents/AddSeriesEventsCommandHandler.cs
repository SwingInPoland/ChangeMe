using ChangeMe.Modules.Events.Application.Configuration.Commands;
using ChangeMe.Modules.Events.Domain.Series;
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

namespace ChangeMe.Modules.Events.Application.Series.SeriesEvent.AddSeriesEvents;

internal class AddSeriesEventsCommandHandler : ICommandHandler<AddSeriesEventsCommand>
{
    private readonly ISeriesRepository _seriesRepository;
    private readonly IExecutionContextAccessor _executionContextAccessor;

    public AddSeriesEventsCommandHandler(
        ISeriesRepository seriesRepository,
        IExecutionContextAccessor executionContextAccessor)
    {
        _seriesRepository = seriesRepository;
        _executionContextAccessor = executionContextAccessor;
    }

    public async Task<Unit> Handle(AddSeriesEventsCommand request, CancellationToken cancellationToken)
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

        var dates = request.Dates.Select(date =>
                SeriesEventDate.Create(
                    date.StartDate.ToDateTimeOffset(),
                    date.EndDate.ToDateTimeOffset()))
            .ToList();

        //TODO: How is it updated?
        series.AddSeriesEvents(
            _executionContextAccessor.UserId,
            EventNames.Create(namesList),
            EventDescriptions.Create(descriptionsList),
            dates,
            EventHost.Create(request.HostName, new Uri(request.HostUrl)),
            EventImage.Create(new Uri(request.ImageUrl)),
            EventUrl.Create(new Uri(request.EventUrl)),
            eventLocation,
            request.IsForFree,
            new HashSet<string>(request.Editors));

        return Unit.Value;
    }
}