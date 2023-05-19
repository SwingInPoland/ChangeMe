using ChangeMe.Modules.Events.Application.Configuration.Commands;
using ChangeMe.Modules.Events.Application.Contracts;
using ChangeMe.Shared.Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ChangeMe.Modules.Events.Infrastructure.Configuration.Processing;

internal class UnitOfWorkCommandHandlerDecorator<TCommand> : ICommandHandler<TCommand> where TCommand : ICommand
{
    private readonly ICommandHandler<TCommand> _decorated;
    private readonly IUnitOfWork _unitOfWork;
    private readonly EventsContext _eventsContext;

    public UnitOfWorkCommandHandlerDecorator(
        ICommandHandler<TCommand> decorated,
        IUnitOfWork unitOfWork,
        EventsContext eventsContext)
    {
        _decorated = decorated;
        _unitOfWork = unitOfWork;
        _eventsContext = eventsContext;
    }

    public async Task<Unit> Handle(TCommand command, CancellationToken cancellationToken)
    {
        await _decorated.Handle(command, cancellationToken);

        if (command is InternalCommandBase)
        {
            var internalCommand =
                await _eventsContext.InternalCommands.FirstOrDefaultAsync(
                    x => x.Id == command.Id,
                    cancellationToken: cancellationToken);

            if (internalCommand is not null)
                internalCommand.ProcessedDate = DateTime.UtcNow;
        }

        await _unitOfWork.CommitAsync(null, cancellationToken);

        return Unit.Value;
    }
}