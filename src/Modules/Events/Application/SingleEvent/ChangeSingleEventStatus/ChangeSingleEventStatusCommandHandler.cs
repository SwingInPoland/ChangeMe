using ChangeMe.Modules.Events.Application.Configuration.Commands;
using ChangeMe.Modules.Events.Domain.SingleEvent;
using ChangeMe.Shared.Application;
using MediatR;

namespace ChangeMe.Modules.Events.Application.SingleEvent.ChangeSingleEventStatus;

internal class ChangeSingleEventStatusCommandHandler : ICommandHandler<ChangeSingleEventStatusCommand>
{
    private readonly ISingleEventRepository _singleEventRepository;
    private readonly IExecutionContextAccessor _executionContextAccessor;

    public ChangeSingleEventStatusCommandHandler(
        ISingleEventRepository singleEventRepository,
        IExecutionContextAccessor executionContextAccessor)
    {
        _singleEventRepository = singleEventRepository;
        _executionContextAccessor = executionContextAccessor;
    }

    public async Task<Unit> Handle(ChangeSingleEventStatusCommand request, CancellationToken cancellationToken)
    {
        var singleEvent = await _singleEventRepository.GetByIdAsync(
            new SingleEventId(request.SingleEventId),
            cancellationToken);

        //TODO: How is it updated?
        singleEvent.ChangeStatus(_executionContextAccessor.UserId, request.Status);

        return Unit.Value;
    }
}