using ChangeMe.Modules.Events.Application.Configuration.Commands;
using ChangeMe.Shared.Application.Data;
using Dapper;
using MediatR;
using Newtonsoft.Json;
using Polly;

namespace ChangeMe.Modules.Events.Infrastructure.Configuration.Processing.InternalCommands;

internal class ProcessInternalCommandsCommandHandler : ICommandHandler<ProcessInternalCommandsCommand>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public ProcessInternalCommandsCommandHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Unit> Handle(ProcessInternalCommandsCommand command, CancellationToken cancellationToken)
    {
        var connection = _sqlConnectionFactory.GetOpenConnection();

        const string sql = "SELECT " +
                           $"[Command].[Id] AS [{nameof(InternalCommandDto.Id)}], " +
                           $"[Command].[Type] AS [{nameof(InternalCommandDto.Type)}], " +
                           $"[Command].[Data] AS [{nameof(InternalCommandDto.Data)}] " +
                           "FROM [events].[InternalCommands] AS [Command] " +
                           "WHERE [Command].[ProcessedDate] IS NULL " +
                           "ORDER BY [Command].[EnqueueDate]";
        var commands = await connection.QueryAsync<InternalCommandDto>(sql);

        var internalCommandsList = commands.AsList();

        var policy = Policy
            .Handle<Exception>()
            .WaitAndRetryAsync(
                new[]
                {
                    TimeSpan.FromSeconds(1),
                    TimeSpan.FromSeconds(2),
                    TimeSpan.FromSeconds(3)
                });

        foreach (var internalCommand in internalCommandsList)
        {
            var result = await policy.ExecuteAndCaptureAsync(() => ProcessCommand(internalCommand));

            if (result.Outcome == OutcomeType.Failure)
                await connection.ExecuteScalarAsync(
                    "UPDATE [events].[InternalCommands] " +
                    "SET ProcessedDate = @NowDate, " +
                    "Error = @Error " +
                    "WHERE [Id] = @Id",
                    new
                    {
                        NowDate = DateTimeOffset.UtcNow,
                        Error = result.FinalException.ToString(),
                        internalCommand.Id
                    });
        }

        return Unit.Value;
    }

    private async Task ProcessCommand(InternalCommandDto internalCommand)
    {
        var type = Assemblies.Application.GetType(internalCommand.Type);
        dynamic commandToProcess = JsonConvert.DeserializeObject(internalCommand.Data, type);

        await CommandsExecutor.ExecuteAsync(commandToProcess);
    }

    private record InternalCommandDto(Guid Id, string Type, string Data);
}