using ChangeMe.Modules.Events.Domain.Series;
using ChangeMe.Modules.Events.Domain.SingleEvent;
using ChangeMe.Shared.Application.Outbox;
using ChangeMe.Shared.Infrastructure.InternalCommands;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ChangeMe.Modules.Events.Infrastructure;

public class EventsContext : DbContext
{
    public DbSet<OutboxMessage> OutboxMessages { get; set; }
    public DbSet<InternalCommand> InternalCommands { get; set; }
    public DbSet<SingleEvent> SingleEvents { get; set; }
    public DbSet<Series> Series { get; set; }

    private readonly ILoggerFactory _loggerFactory;

    public EventsContext(DbContextOptions options, ILoggerFactory loggerFactory)
        : base(options)
    {
        _loggerFactory = loggerFactory;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        optionsBuilder.UseLoggerFactory(_loggerFactory).EnableSensitiveDataLogging();

    protected override void OnModelCreating(ModelBuilder modelBuilder) =>
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
}