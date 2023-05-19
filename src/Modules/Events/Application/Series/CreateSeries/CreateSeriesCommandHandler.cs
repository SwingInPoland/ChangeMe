using ChangeMe.Modules.Events.Application.Configuration.Commands;
using ChangeMe.Modules.Events.Domain.Series;
using ChangeMe.Modules.Events.Domain.Series.ValueObjects.SeriesDescriptions;
using ChangeMe.Modules.Events.Domain.Series.ValueObjects.SeriesEventDate;
using ChangeMe.Modules.Events.Domain.Series.ValueObjects.SeriesNames;
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

namespace ChangeMe.Modules.Events.Application.Series.CreateSeries;

internal class CreateSeriesCommandHandler : ICommandHandler<CreateSeriesCommand, Guid>
{
    private readonly ISeriesRepository _seriesRepository;
    private readonly IExecutionContextAccessor _executionContextAccessor;

    public CreateSeriesCommandHandler(
        ISeriesRepository seriesRepository,
        IExecutionContextAccessor executionContextAccessor)
    {
        _seriesRepository = seriesRepository;
        _executionContextAccessor = executionContextAccessor;
    }

    public async Task<Guid> Handle(CreateSeriesCommand request, CancellationToken cancellationToken)
    {
        var seriesNamesList = new List<TranslationValueBase>();
        if (request.SeriesNames.TryGetValue("pl", out var polishSeriesName))
            seriesNamesList.Add(PolishSeriesName.Create(polishSeriesName));
        if (request.SeriesNames.TryGetValue("en", out var englishSeriesName))
            seriesNamesList.Add(EnglishSeriesName.Create(englishSeriesName));
        if (request.SeriesNames.TryGetValue("uk", out var ukrainianSeriesName))
            seriesNamesList.Add(UkrainianSeriesName.Create(ukrainianSeriesName));

        var seriesDescriptionsList = new List<TranslationValueBase>();
        if (request.SeriesDescriptions.TryGetValue("pl", out var polishSeriesDescription))
            seriesDescriptionsList.Add(PolishSeriesDescription.Create(polishSeriesDescription));
        if (request.SeriesDescriptions.TryGetValue("en", out var englishSeriesDescription))
            seriesDescriptionsList.Add(EnglishSeriesDescription.Create(englishSeriesDescription));
        if (request.SeriesDescriptions.TryGetValue("uk", out var ukrainianSeriesDescription))
            seriesDescriptionsList.Add(UkrainianSeriesDescription.Create(ukrainianSeriesDescription));

        var eventNamesList = new List<TranslationValueBase>();
        if (request.EventNames.TryGetValue("pl", out var polishEventName))
            eventNamesList.Add(PolishEventName.Create(polishEventName));
        if (request.EventNames.TryGetValue("en", out var englishEventName))
            eventNamesList.Add(EnglishEventName.Create(englishEventName));
        if (request.EventNames.TryGetValue("uk", out var ukrainianEventName))
            eventNamesList.Add(UkrainianEventName.Create(ukrainianEventName));

        var eventDescriptionsList = new List<TranslationValueBase>();
        if (request.EventDescriptions.TryGetValue("pl", out var polishEventDescription))
            eventDescriptionsList.Add(PolishEventDescription.Create(polishEventDescription));
        if (request.EventDescriptions.TryGetValue("en", out var englishEventDescription))
            eventDescriptionsList.Add(EnglishEventDescription.Create(englishEventDescription));
        if (request.EventDescriptions.TryGetValue("uk", out var ukrainianEventDescription))
            eventDescriptionsList.Add(UkrainianEventDescription.Create(ukrainianEventDescription));

        var eventDates = request.EventDates.Select(date =>
                SeriesEventDate.Create(
                    date.StartDate.ToDateTimeOffset(),
                    date.EndDate.ToDateTimeOffset()))
            .ToList();

        var eventLocation = EventLocation.Create(
            Coordinates.Create(request.EventLocationCoordinatesLatitude, request.EventLocationCoordinatesLongitude),
            City.Create(request.EventLocationCity),
            Province.Create(request.EventLocationProvince),
            PostalCode.Create(request.EventLocationPostalCode),
            LocationName.TryCreate(request.EventLocationName),
            Street.TryCreate(request.EventLocationStreetName, request.EventLocationStreetNumber,
                request.EventLocationStreetAdditionalInfo));

        var series = Domain.Series.Series.Create(
            _executionContextAccessor.UserId,
            SeriesNames.Create(seriesNamesList),
            SeriesDescriptions.Create(seriesDescriptionsList),
            new HashSet<string>(request.SeriesEditors),
            EventNames.Create(eventNamesList),
            EventDescriptions.Create(eventDescriptionsList),
            eventDates,
            EventHost.Create(request.EventHostName, new Uri(request.EventHostUrl)),
            EventImage.Create(new Uri(request.EventImageUrl)),
            EventUrl.Create(new Uri(request.EventUrl)),
            eventLocation,
            request.EventIsForFree,
            new HashSet<string>(request.EventEditors));

        // TODO: How does it handle creating series events?
        await _seriesRepository.AddAsync(series, cancellationToken);

        return series.Id.Value;
    }
}