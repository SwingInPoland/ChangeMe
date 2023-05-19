namespace ChangeMe.Modules.Events.Domain.Series;

public interface ISeriesRepository
{
    Task AddAsync(Series series, CancellationToken cancellationToken);

    Task<Series?> GetByIdAsync(SeriesId id, CancellationToken cancellationToken);
}