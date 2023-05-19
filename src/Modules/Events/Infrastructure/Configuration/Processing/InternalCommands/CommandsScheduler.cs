using ChangeMe.Modules.Events.Application.Configuration.Commands;
using ChangeMe.Modules.Events.Application.Contracts;
using ChangeMe.Shared.Application.Data;
using ChangeMe.Shared.Infrastructure.Serialization;
using Dapper;
using Newtonsoft.Json;

namespace ChangeMe.Modules.Events.Infrastructure.Configuration.Processing.InternalCommands;

public class CommandsScheduler : ICommandsScheduler
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public CommandsScheduler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task EnqueueAsync(ICommand command)
    {
        var connection = _sqlConnectionFactory.GetOpenConnection();

        const string sqlInsert =
            "INSERT INTO [events].[InternalCommands] ([Id], [EnqueueDate] , [Type], [Data]) VALUES " +
            "(@Id, @EnqueueDate, @Type, @Data)";

        await connection.ExecuteAsync(sqlInsert, new
        {
            command.Id,
            EnqueueDate = DateTimeOffset.UtcNow,
            Type = command.GetType().FullName,
            Data = JsonConvert.SerializeObject(command, new JsonSerializerSettings
            {
                ContractResolver = new AllPropertiesContractResolver()
            })
        });
    }
}