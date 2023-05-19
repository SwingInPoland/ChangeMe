using ChangeMe.Shared.Extensions;
using FluentValidation;

namespace ChangeMe.Modules.Events.Application.SingleEvent.ChangeSingleEventEditors;

internal class ChangeSingleEventEditorsCommandValidator : AbstractValidator<ChangeSingleEventEditorsCommand>
{
    public ChangeSingleEventEditorsCommandValidator()
    {
        RuleFor(x => x.SingleEventId)
            .NotEmpty().WithMessage("Single event ID must not be empty.");

        RuleFor(x => x.UserIds)
            .NotEmpty().WithMessage("User IDs must not be empty.")
            .Must(x => x.IsUnique()).WithMessage("User IDs must be unique.");
    }
}