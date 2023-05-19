using FluentValidation;

namespace ChangeMe.Modules.Events.Application.Series.DeleteSeries;

internal class DeleteSeriesCommandValidator : AbstractValidator<DeleteSeriesCommand>
{
    public DeleteSeriesCommandValidator()
    {
        RuleFor(x => x.SeriesId)
            .NotEmpty().WithMessage("Series ID must not be empty.");
    }
}