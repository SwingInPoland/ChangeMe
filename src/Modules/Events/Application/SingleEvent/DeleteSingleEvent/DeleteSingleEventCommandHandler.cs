using ChangeMe.Modules.Events.Application.Configuration.Commands;
using ChangeMe.Modules.Events.Domain.SingleEvent;
using ChangeMe.Shared.Application;
using MediatR;

namespace ChangeMe.Modules.Events.Application.SingleEvent.DeleteSingleEvent;

internal class DeleteSingleEventCommandHandler : ICommandHandler<DeleteSingleEventCommand>
{
    private readonly ISingleEventRepository _singleEventRepository;
    private readonly IExecutionContextAccessor _executionContextAccessor;

    public DeleteSingleEventCommandHandler(
        ISingleEventRepository singleEventRepository,
        IExecutionContextAccessor executionContextAccessor)
    {
        _singleEventRepository = singleEventRepository;
        _executionContextAccessor = executionContextAccessor;
    }

    public async Task<Unit> Handle(DeleteSingleEventCommand request, CancellationToken cancellationToken)
    {
        var singleEvent = await _singleEventRepository.GetByIdAsync(
            new SingleEventId(request.SingleEventId),
            cancellationToken);

        // TODO: How is it deleted?
        singleEvent.Delete(_executionContextAccessor.UserId);

        return Unit.Value;
    }
}