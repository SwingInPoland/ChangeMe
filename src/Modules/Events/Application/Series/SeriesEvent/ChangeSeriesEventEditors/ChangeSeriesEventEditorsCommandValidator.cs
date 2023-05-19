using FluentValidation;

namespace ChangeMe.Modules.Events.Application.Series.SeriesEvent.ChangeSeriesEventEditors;

internal class ChangeSeriesEventEditorsCommandValidator : AbstractValidator<ChangeSeriesEventEditorsCommand>
{
    public ChangeSeriesEventEditorsCommandValidator()
    {
        RuleFor(x => x.SeriesId)
            .NotEmpty().WithMessage("Series ID cannot be empty.");

        RuleFor(x => x.SeriesEventId)
            .NotEmpty().WithMessage("Series event ID cannot be empty.");

        RuleFor(x => x.UserIds)
            .Must(x => x.Length == x.Distinct().Count())
            .WithMessage("UserIds must be unique.");
    }
}