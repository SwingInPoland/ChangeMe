using ChangeMe.Modules.Events.Application.Configuration.Commands;
using ChangeMe.Modules.Events.Domain.Series;
using ChangeMe.Shared.Application;
using MediatR;

namespace ChangeMe.Modules.Events.Application.Series.DeleteSeries;

internal class DeleteSeriesCommandHandler : ICommandHandler<DeleteSeriesCommand>
{
    private readonly ISeriesRepository _seriesRepository;
    private readonly IExecutionContextAccessor _executionContextAccessor;

    public DeleteSeriesCommandHandler(
        ISeriesRepository seriesRepository,
        IExecutionContextAccessor executionContextAccessor)
    {
        _seriesRepository = seriesRepository;
        _executionContextAccessor = executionContextAccessor;
    }

    public async Task<Unit> Handle(DeleteSeriesCommand request, CancellationToken cancellationToken)
    {
        var series = await _seriesRepository.GetByIdAsync(new SeriesId(request.SeriesId), cancellationToken);

        // TODO: How is it deleted?
        series.Delete(_executionContextAccessor.UserId);

        return Unit.Value;
    }
}