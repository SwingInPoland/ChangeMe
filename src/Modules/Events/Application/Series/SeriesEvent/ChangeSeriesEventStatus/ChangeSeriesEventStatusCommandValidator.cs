using FluentValidation;

namespace ChangeMe.Modules.Events.Application.Series.SeriesEvent.ChangeSeriesEventStatus;

internal class ChangeSeriesEventStatusCommandValidator : AbstractValidator<ChangeSeriesEventStatusCommand>
{
    public ChangeSeriesEventStatusCommandValidator()
    {
        RuleFor(x => x.SeriesId)
            .NotEmpty().WithMessage("Series ID must not be empty.");

        RuleFor(x => x.SeriesEventId)
            .NotEmpty().WithMessage("Series event ID must not be empty.");

        RuleFor(x => x.Status)
            .NotEmpty().WithMessage("Status must not be empty.")
            .MaximumLength(1000).WithMessage("Status must not exceed 1000 characters.");
    }
}