using Autofac;
using ChangeMe.Shared.Application.Data;
using ChangeMe.Shared.Infrastructure.EventBus;
using ChangeMe.Shared.Infrastructure.Serialization;
using Dapper;
using Newtonsoft.Json;

namespace ChangeMe.Modules.Events.Infrastructure.Configuration.EventsBus;

internal class IntegrationEventGenericHandler<TIntegrationEvent> : IIntegrationEventHandler<TIntegrationEvent>
    where TIntegrationEvent : IntegrationEvent
{
    public async Task Handle(TIntegrationEvent @event)
    {
        using var scope = EventsCompositionRoot.BeginLifetimeScope();
        using var connection = scope.Resolve<ISqlConnectionFactory>().GetOpenConnection();

        var type = @event.GetType().FullName;
        var data = JsonConvert.SerializeObject(@event, new JsonSerializerSettings
        {
            ContractResolver = new AllPropertiesContractResolver()
        });

        const string sql = "INSERT INTO [events].[InboxMessages] (Id, OccurredOn, Type, Data) " +
                           "VALUES (@Id, @OccurredOn, @Type, @Data)";

        await connection.ExecuteScalarAsync(sql, new
        {
            @event.Id,
            @event.OccurredOn,
            type,
            data
        });
    }
}