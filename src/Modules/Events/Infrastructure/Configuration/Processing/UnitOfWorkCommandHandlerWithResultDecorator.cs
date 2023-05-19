using ChangeMe.Modules.Events.Application.Configuration.Commands;
using ChangeMe.Modules.Events.Application.Contracts;
using ChangeMe.Shared.Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace ChangeMe.Modules.Events.Infrastructure.Configuration.Processing;

internal class UnitOfWorkCommandHandlerWithResultDecorator<TCommand, TResult> : ICommandHandler<TCommand, TResult>
    where TCommand : ICommand<TResult>
{
    private readonly ICommandHandler<TCommand, TResult> _decorated;
    private readonly IUnitOfWork _unitOfWork;
    private readonly EventsContext _eventsContext;

    public UnitOfWorkCommandHandlerWithResultDecorator(
        ICommandHandler<TCommand, TResult> decorated,
        IUnitOfWork unitOfWork,
        EventsContext eventsContext)
    {
        _decorated = decorated;
        _unitOfWork = unitOfWork;
        _eventsContext = eventsContext;
    }

    public async Task<TResult> Handle(TCommand command, CancellationToken cancellationToken)
    {
        var result = await _decorated.Handle(command, cancellationToken);

        if (command is InternalCommandBase<TResult>)
        {
            var internalCommand =
                await _eventsContext.InternalCommands.FirstOrDefaultAsync(x => x.Id == command.Id,
                    cancellationToken: cancellationToken);

            if (internalCommand is not null)
                internalCommand.ProcessedDate = DateTime.UtcNow;
        }

        await _unitOfWork.CommitAsync(null, cancellationToken);

        return result;
    }
}