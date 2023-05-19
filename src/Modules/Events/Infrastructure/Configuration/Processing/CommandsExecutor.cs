using Autofac;
using ChangeMe.Modules.Events.Application.Contracts;
using MediatR;

namespace ChangeMe.Modules.Events.Infrastructure.Configuration.Processing;

internal static class CommandsExecutor
{
    internal static async Task ExecuteAsync(ICommand command)
    {
        using var scope = EventsCompositionRoot.BeginLifetimeScope();
        var mediator = scope.Resolve<IMediator>();
        await mediator.Send(command);
    }

    internal static async Task<TResult> ExecuteAsync<TResult>(ICommand<TResult> command)
    {
        using var scope = EventsCompositionRoot.BeginLifetimeScope();
        var mediator = scope.Resolve<IMediator>();
        return await mediator.Send(command);
    }
}