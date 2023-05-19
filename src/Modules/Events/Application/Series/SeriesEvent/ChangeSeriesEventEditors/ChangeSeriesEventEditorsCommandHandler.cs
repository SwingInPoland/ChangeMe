using ChangeMe.Modules.Events.Application.Configuration.Commands;
using ChangeMe.Modules.Events.Domain.Series;
using ChangeMe.Modules.Events.Domain.Series.Entities.SeriesEvent;
using ChangeMe.Shared.Application;
using MediatR;

namespace ChangeMe.Modules.Events.Application.Series.SeriesEvent.ChangeSeriesEventEditors;

internal class ChangeSeriesEventEditorsCommandHandler : ICommandHandler<ChangeSeriesEventEditorsCommand>
{
    private readonly ISeriesRepository _seriesRepository;
    private readonly IExecutionContextAccessor _executionContextAccessor;

    public ChangeSeriesEventEditorsCommandHandler(
        ISeriesRepository seriesRepository,
        IExecutionContextAccessor executionContextAccessor)
    {
        _seriesRepository = seriesRepository;
        _executionContextAccessor = executionContextAccessor;
    }

    public async Task<Unit> Handle(ChangeSeriesEventEditorsCommand request, CancellationToken cancellationToken)
    {
        var series = await _seriesRepository.GetByIdAsync(new SeriesId(request.SeriesId), cancellationToken);

        //TODO: How is it updated?
        series.ChangeSeriesEventEditors(_executionContextAccessor.UserId,
            new SeriesEventId(request.SeriesEventId),
            new HashSet<string>(request.UserIds));

        return Unit.Value;
    }
}