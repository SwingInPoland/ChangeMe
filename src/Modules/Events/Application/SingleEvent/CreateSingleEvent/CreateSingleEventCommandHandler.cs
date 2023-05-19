using ChangeMe.Modules.Events.Application.Configuration.Commands;
using ChangeMe.Modules.Events.Domain.SingleEvent;
using ChangeMe.Modules.Events.Domain.SingleEvent.ValueObjects.SingleEventDate;
using ChangeMe.Modules.Events.Domain.SingleEvent.ValueObjects.SingleEventStatus;
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

namespace ChangeMe.Modules.Events.Application.SingleEvent.CreateSingleEvent;

internal class CreateSingleEventCommandHandler : ICommandHandler<CreateSingleEventCommand, Guid>
{
    private readonly ISingleEventRepository _singleEventRepository;
    private readonly IExecutionContextAccessor _executionContextAccessor;

    public CreateSingleEventCommandHandler(
        ISingleEventRepository singleEventRepository,
        IExecutionContextAccessor executionContextAccessor)
    {
        _singleEventRepository = singleEventRepository;
        _executionContextAccessor = executionContextAccessor;
    }

    public async Task<Guid> Handle(CreateSingleEventCommand request, CancellationToken cancellationToken)
    {
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

        var singleEvent = Domain.SingleEvent.SingleEvent.Create(
            _executionContextAccessor.UserId,
            EventNames.Create(namesList),
            EventDescriptions.Create(descriptionsList),
            SingleEventDate.Create(request.StartDate.ToDateTimeOffset(), request.EndDate.ToDateTimeOffset()),
            EventHost.Create(request.HostName, new Uri(request.HostUrl)),
            EventImage.Create(new Uri(request.ImageUrl)),
            EventUrl.Create(new Uri(request.EventUrl)),
            eventLocation,
            request.IsForFree,
            SingleEventStatus.Create(request.Status),
            new HashSet<string>(request.Editors));

        await _singleEventRepository.AddAsync(singleEvent, cancellationToken);

        return singleEvent.Id.Value;
    }
}