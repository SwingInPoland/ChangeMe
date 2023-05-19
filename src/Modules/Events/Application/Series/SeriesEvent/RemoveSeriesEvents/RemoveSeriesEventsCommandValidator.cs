using ChangeMe.Shared.Extensions;
using FluentValidation;

namespace ChangeMe.Modules.Events.Application.Series.SeriesEvent.RemoveSeriesEvents;

internal class RemoveSeriesEventsCommandValidator : AbstractValidator<RemoveSeriesEventsCommand>
{
    public RemoveSeriesEventsCommandValidator()
    {
        RuleFor(x => x.SeriesId)
            .NotEmpty().WithMessage("Series ID must not be empty.");

        RuleFor(x => x.SeriesEventIds)
            .Must(x => x.IsUnique())
            .WithMessage("Series event IDs must be unique.");
    }
}