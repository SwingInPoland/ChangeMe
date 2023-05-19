using FluentValidation;

namespace ChangeMe.Modules.Events.Application.SingleEvent.ChangeSingleEventStatus;

internal class ChangeSingleEventStatusCommandValidator : AbstractValidator<ChangeSingleEventStatusCommand>
{
    public ChangeSingleEventStatusCommandValidator()
    {
        RuleFor(x => x.SingleEventId)
            .NotEmpty().WithMessage("Single event ID must not be empty.");

        RuleFor(x => x.Status)
            .NotEmpty().WithMessage("Status must not be empty.");
    }
}