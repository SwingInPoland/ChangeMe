using ChangeMe.Modules.Events.Application.Configuration.Commands;
using ChangeMe.Modules.Events.Domain.Series;
using ChangeMe.Modules.Events.Domain.Series.Entities.SeriesEvent;
using ChangeMe.Shared.Application;
using MediatR;

namespace ChangeMe.Modules.Events.Application.Series.SeriesEvent.ChangeSeriesEventStatus;

internal class ChangeSeriesEventStatusCommandHandler : ICommandHandler<ChangeSeriesEventStatusCommand>
{
    private readonly ISeriesRepository _seriesRepository;
    private readonly IExecutionContextAccessor _executionContextAccessor;

    public ChangeSeriesEventStatusCommandHandler(
        ISeriesRepository seriesRepository,
        IExecutionContextAccessor executionContextAccessor)
    {
        _seriesRepository = seriesRepository;
        _executionContextAccessor = executionContextAccessor;
    }

    public async Task<Unit> Handle(ChangeSeriesEventStatusCommand request, CancellationToken cancellationToken)
    {
        var series = await _seriesRepository.GetByIdAsync(new SeriesId(request.SeriesId), cancellationToken);

        //TODO: How is it updated?
        series.ChangeSeriesEventStatus(_executionContextAccessor.UserId, new SeriesEventId(request.SeriesEventId),
            request.Status);

        return Unit.Value;
    }
}