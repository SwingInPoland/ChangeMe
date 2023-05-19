using ChangeMe.Modules.Events.Application.Configuration.Commands;
using ChangeMe.Modules.Events.Application.Contracts;
using ChangeMe.Shared.Application;
using FluentValidation;

namespace ChangeMe.Modules.Events.Infrastructure.Configuration.Processing;

internal class ValidationCommandHandlerWithResultDecorator<TCommand, TResult> : ICommandHandler<TCommand, TResult>
    where TCommand : ICommand<TResult>
{
    private readonly IList<IValidator<TCommand>> _validators;
    private readonly ICommandHandler<TCommand, TResult> _decorated;

    public ValidationCommandHandlerWithResultDecorator(
        IList<IValidator<TCommand>> validators,
        ICommandHandler<TCommand, TResult> decorated)
    {
        _validators = validators;
        _decorated = decorated;
    }

    public Task<TResult> Handle(TCommand command, CancellationToken cancellationToken)
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