using FluentValidation;

namespace ChangeMe.Modules.Events.Application.Series.ChangeSeriesMainAttributes;

internal class ChangeSeriesMainAttributesCommandValidator : AbstractValidator<ChangeSeriesMainAttributesCommand>
{
    public ChangeSeriesMainAttributesCommandValidator()
    {
        RuleFor(x => x.SeriesId)
            .NotEmpty().WithMessage("Series ID must not be empty.");

        RuleFor(x => x.Names)
            .NotEmpty().WithMessage("Names must not be empty.")
            .Must(names => names.ContainsKey("pl")).WithMessage("Polish name must be provided.");

        RuleFor(x => x.Descriptions)
            .NotEmpty().WithMessage("Descriptions must not be empty.")
            .Must(descriptions => descriptions.ContainsKey("pl")).WithMessage("Polish description must be provided.");
    }
}