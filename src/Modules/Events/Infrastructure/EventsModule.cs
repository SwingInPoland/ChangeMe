using Autofac;
using ChangeMe.Modules.Events.Application.Contracts;
using ChangeMe.Modules.Events.Infrastructure.Configuration;
using ChangeMe.Modules.Events.Infrastructure.Configuration.Processing;
using MediatR;

namespace ChangeMe.Modules.Events.Infrastructure;

public class EventsModule : IEventsModule
{
    public async Task<TResult> ExecuteCommandAsync<TResult>(ICommand<TResult> command) =>
        await CommandsExecutor.ExecuteAsync(command);

    public async Task ExecuteCommandAsync(ICommand command) => await CommandsExecutor.ExecuteAsync(command);

    public async Task<TResult> ExecuteQueryAsync<TResult>(IQuery<TResult> query)
    {
        using var scope = EventsCompositionRoot.BeginLifetimeScope();
        var mediator = scope.Resolve<IMediator>();
        return await mediator.Send(query);
    }
}