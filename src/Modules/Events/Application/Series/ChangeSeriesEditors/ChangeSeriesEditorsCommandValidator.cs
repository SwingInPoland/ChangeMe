using ChangeMe.Shared.Extensions;
using FluentValidation;

namespace ChangeMe.Modules.Events.Application.Series.ChangeSeriesEditors;

internal class ChangeSeriesEditorsCommandValidator : AbstractValidator<ChangeSeriesEditorsCommand>
{
    public ChangeSeriesEditorsCommandValidator()
    {
        RuleFor(x => x.SeriesId)
            .NotEmpty().WithMessage("Series ID must not be empty.");

        RuleFor(x => x.UserIds)
            .NotEmpty().WithMessage("User IDs must not be empty.")
            .Must(x => x.IsUnique()).WithMessage("User IDs must be unique.");
    }
}