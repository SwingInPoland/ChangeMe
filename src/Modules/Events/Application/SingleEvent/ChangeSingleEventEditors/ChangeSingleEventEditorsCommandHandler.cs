using ChangeMe.Modules.Events.Application.Configuration.Commands;
using ChangeMe.Modules.Events.Domain.SingleEvent;
using ChangeMe.Shared.Application;
using MediatR;

namespace ChangeMe.Modules.Events.Application.SingleEvent.ChangeSingleEventEditors;

internal class ChangeSingleEventEditorsCommandHandler : ICommandHandler<ChangeSingleEventEditorsCommand>
{
    private readonly ISingleEventRepository _singleEventRepository;
    private readonly IExecutionContextAccessor _executionContextAccessor;

    public ChangeSingleEventEditorsCommandHandler(
        ISingleEventRepository singleEventRepository,
        IExecutionContextAccessor executionContextAccessor)
    {
        _singleEventRepository = singleEventRepository;
        _executionContextAccessor = executionContextAccessor;
    }

    public async Task<Unit> Handle(ChangeSingleEventEditorsCommand request, CancellationToken cancellationToken)
    {
        var singleEvent =
            await _singleEventRepository.GetByIdAsync(new SingleEventId(request.SingleEventId), cancellationToken);

        //TODO: How is it updated?
        singleEvent.ChangeEditors(_executionContextAccessor.UserId, new HashSet<string>(request.UserIds));

        return Unit.Value;
    }
}