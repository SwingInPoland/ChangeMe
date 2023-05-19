using ChangeMe.Modules.Events.Application.Configuration.Commands;
using ChangeMe.Modules.Events.Domain.Series;
using ChangeMe.Modules.Events.Domain.Series.ValueObjects.SeriesDescriptions;
using ChangeMe.Modules.Events.Domain.Series.ValueObjects.SeriesNames;
using ChangeMe.Modules.Events.Domain.ValueObjects.TranslationBase;
using ChangeMe.Shared.Application;
using MediatR;

namespace ChangeMe.Modules.Events.Application.Series.ChangeSeriesMainAttributes;

internal class ChangeSeriesMainAttributesCommandHandler : ICommandHandler<ChangeSeriesMainAttributesCommand>
{
    private readonly ISeriesRepository _seriesRepository;
    private readonly IExecutionContextAccessor _executionContextAccessor;

    public ChangeSeriesMainAttributesCommandHandler(
        ISeriesRepository seriesRepository,
        IExecutionContextAccessor executionContextAccessor)
    {
        _seriesRepository = seriesRepository;
        _executionContextAccessor = executionContextAccessor;
    }

    public async Task<Unit> Handle(ChangeSeriesMainAttributesCommand request, CancellationToken cancellationToken)
    {
        var series = await _seriesRepository.GetByIdAsync(new SeriesId(request.SeriesId), cancellationToken);

        var namesList = new List<TranslationValueBase>();
        if (request.Names.TryGetValue("pl", out var polishName))
            namesList.Add(PolishSeriesName.Create(polishName));
        if (request.Names.TryGetValue("en", out var englishName))
            namesList.Add(EnglishSeriesName.Create(englishName));
        if (request.Names.TryGetValue("uk", out var ukrainianName))
            namesList.Add(UkrainianSeriesName.Create(ukrainianName));

        var descriptionsList = new List<TranslationValueBase>();
        if (request.Descriptions.TryGetValue("pl", out var polishDescription))
            descriptionsList.Add(PolishSeriesDescription.Create(polishDescription));
        if (request.Descriptions.TryGetValue("en", out var englishDescription))
            descriptionsList.Add(EnglishSeriesDescription.Create(englishDescription));
        if (request.Descriptions.TryGetValue("uk", out var ukrainianDescription))
            descriptionsList.Add(UkrainianSeriesDescription.Create(ukrainianDescription));

        //TODO: How is it updated?
        series.ChangeMainAttributes(
            _executionContextAccessor.UserId,
            SeriesNames.Create(namesList),
            SeriesDescriptions.Create(descriptionsList));

        return Unit.Value;
    }
}