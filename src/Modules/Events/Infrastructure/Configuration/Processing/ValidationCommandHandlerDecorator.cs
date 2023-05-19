using ChangeMe.Modules.Events.Application.Configuration.Commands;
using ChangeMe.Modules.Events.Application.Contracts;
using ChangeMe.Shared.Application;
using FluentValidation;
using MediatR;

namespace ChangeMe.Modules.Events.Infrastructure.Configuration.Processing;

internal class ValidationCommandHandlerDecorator<TCommand> : ICommandHandler<TCommand> where TCommand : ICommand
{
    private readonly IList<IValidator<TCommand>> _validators;
    private readonly ICommandHandler<TCommand> _decorated;

    public ValidationCommandHandlerDecorator(IList<IValidator<TCommand>> validators, ICommandHandler<TCommand> decorated)
    {
        _validators = validators;
        _decorated = decorated;
    }

    public Task<Unit> Handle(TCommand command, CancellationToken cancellationToken)
    {
        var errors = _validators
            .Select(v => v.Validate(command))
            .SelectMany(result => result.Errors)
            .Where(error => error != null)
            .ToList();

        if (errors.Any())
            throw new InvalidCommandException(errors.Select(x => x.ErrorMessage));

        return _decorated.Handle(command, cancellationToken);
    }
}