using FluentValidation;

namespace ChangeMe.Modules.Events.Application.SingleEvent.DeleteSingleEvent;

internal class DeleteSingleEventCommandValidator : AbstractValidator<DeleteSingleEventCommand>
{
    public DeleteSingleEventCommandValidator()
    {
        RuleFor(x => x.SingleEventId)
            .NotEmpty().WithMessage("Single event ID must not be empty.");
    }
}