using ChangeMe.Modules.Events.Application.Configuration.Commands;
using ChangeMe.Shared.Application.Data;
using Dapper;
using MediatR;
using Newtonsoft.Json;

namespace ChangeMe.Modules.Events.Infrastructure.Configuration.Processing.Inbox;

internal class ProcessInboxCommandHandler : ICommandHandler<ProcessInboxCommand>
{
    private readonly IMediator _mediator;
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public ProcessInboxCommandHandler(IMediator mediator, ISqlConnectionFactory sqlConnectionFactory)
    {
        _mediator = mediator;
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Unit> Handle(ProcessInboxCommand command, CancellationToken cancellationToken)
    {
        var connection = _sqlConnectionFactory.GetOpenConnection();
        const string sql = "SELECT " +
                           $"[InboxMessage].[Id] AS [{nameof(InboxMessageDto.Id)}], " +
                           $"[InboxMessage].[Type] AS [{nameof(InboxMessageDto.Type)}], " +
                           $"[InboxMessage].[Data] AS [{nameof(InboxMessageDto.Data)}] " +
                           "FROM [events].[InboxMessages] AS [InboxMessage] " +
                           "WHERE [InboxMessage].[ProcessedDate] IS NULL " +
                           "ORDER BY [InboxMessage].[OccurredOn]";

        var messages = await connection.QueryAsync<InboxMessageDto>(sql);

        const string sqlUpdateProcessedDate = "UPDATE [events].[InboxMessages] " +
                                              "SET [ProcessedDate] = @Date " +
                                              "WHERE [Id] = @Id";

        foreach (var message in messages)
        {
            var messageAssembly = AppDomain.CurrentDomain.GetAssemblies()
                .SingleOrDefault(assembly => message.Type.Contains(assembly.GetName().Name));

            var type = messageAssembly.GetType(message.Type);
            var request = JsonConvert.DeserializeObject(message.Data, type);

            try
            {
                await _mediator.Publish<INotification>((INotification)request, cancellationToken);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }

            await connection.ExecuteScalarAsync(sqlUpdateProcessedDate, new
            {
                Date = DateTime.UtcNow,
                message.Id
            });
        }

        return Unit.Value;
    }
}