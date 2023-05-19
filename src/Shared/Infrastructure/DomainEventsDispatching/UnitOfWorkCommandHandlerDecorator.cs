using ChangeMe.Shared.Infrastructure.UnitOfWork;
using MediatR;

namespace ChangeMe.Shared.Infrastructure.DomainEventsDispatching;

public class UnitOfWorkCommandHandlerDecorator<TRequest> : IRequestHandler<TRequest, Unit> where TRequest : IRequest<Unit>
{
    private readonly IRequestHandler<TRequest, Unit> _decorated;
    private readonly IUnitOfWork _unitOfWork;

    public UnitOfWorkCommandHandlerDecorator(IRequestHandler<TRequest, Unit> decorated, IUnitOfWork unitOfWork)
    {
        _decorated = decorated;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(TRequest request, CancellationToken cancellationToken)
    {
        await _decorated.Handle(request, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken: cancellationToken);

        return Unit.Value;
    }
}