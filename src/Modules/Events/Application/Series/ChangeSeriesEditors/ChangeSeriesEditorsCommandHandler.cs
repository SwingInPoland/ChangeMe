using ChangeMe.Modules.Events.Application.Configuration.Commands;
using ChangeMe.Modules.Events.Domain.Series;
using ChangeMe.Shared.Application;
using MediatR;

namespace ChangeMe.Modules.Events.Application.Series.ChangeSeriesEditors;

internal class ChangeSeriesEditorsCommandHandler : ICommandHandler<ChangeSeriesEditorsCommand>
{
    private readonly ISeriesRepository _seriesRepository;
    private readonly IExecutionContextAccessor _executionContextAccessor;

    public ChangeSeriesEditorsCommandHandler(
        ISeriesRepository seriesRepository,
        IExecutionContextAccessor executionContextAccessor)
    {
        _seriesRepository = seriesRepository;
        _executionContextAccessor = executionContextAccessor;
    }

    public async Task<Unit> Handle(ChangeSeriesEditorsCommand request, CancellationToken cancellationToken)
    {
        var series = await _seriesRepository.GetByIdAsync(new SeriesId(request.SeriesId), cancellationToken);

        //TODO: How is it updated?
        series.ChangeEditors(_executionContextAccessor.UserId, new HashSet<string>(request.UserIds));

        return Unit.Value;
    }
}