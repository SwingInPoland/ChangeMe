using ChangeMe.Modules.Events.Application.Configuration.Commands;
using ChangeMe.Modules.Events.Domain.Series;
using ChangeMe.Modules.Events.Domain.Series.Entities.SeriesEvent;
using ChangeMe.Shared.Application;
using MediatR;

namespace ChangeMe.Modules.Events.Application.Series.SeriesEvent.RemoveSeriesEvents;

internal class RemoveSeriesEventsCommandHandler : ICommandHandler<RemoveSeriesEventsCommand>
{
    private readonly ISeriesRepository _seriesRepository;
    private readonly IExecutionContextAccessor _executionContextAccessor;

    public RemoveSeriesEventsCommandHandler(
        ISeriesRepository seriesRepository,
        IExecutionContextAccessor executionContextAccessor)
    {
        _seriesRepository = seriesRepository;
        _executionContextAccessor = executionContextAccessor;
    }

    public async Task<Unit> Handle(RemoveSeriesEventsCommand request, CancellationToken cancellationToken)
    {
        var series = await _seriesRepository.GetByIdAsync(new SeriesId(request.SeriesId), cancellationToken);

        var seriesEventIds = request.SeriesEventIds.Select(id => new SeriesEventId(id)).ToHashSet();

        // TODO: How is it removed?
        series.RemoveSeriesEvents(_executionContextAccessor.UserId, seriesEventIds);

        return Unit.Value;
    }
}