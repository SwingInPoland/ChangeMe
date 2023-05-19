using ChangeMe.Shared.Domain;
using ChangeMe.Shared.Extensions;
using FluentValidation;

namespace ChangeMe.Modules.Events.Application.Series.CreateSeries;

internal class CreateSeriesCommandValidator : AbstractValidator<CreateSeriesCommand>
{
    public CreateSeriesCommandValidator()
    {
        RuleFor(x => x.SeriesNames)
            .NotEmpty().WithMessage("Series names must not be empty.")
            .Must(names => names.ContainsKey("pl")).WithMessage("Polish series name must be provided.");

        RuleFor(x => x.SeriesDescriptions)
            .NotEmpty().WithMessage("Series descriptions must not be empty.")
            .Must(descriptions => descriptions.ContainsKey("pl"))
            .WithMessage("Polish series description must be provided.");

        RuleFor(x => x.SeriesEditors)
            .Must(x => x.IsUnique()).WithMessage("Series editors must be unique.");

        RuleFor(x => x.EventNames)
            .NotEmpty().WithMessage("Event names must not be empty.")
            .Must(names => names.ContainsKey("pl")).WithMessage("Polish event name must be provided.");

        RuleFor(x => x.EventDescriptions)
            .NotEmpty().WithMessage("Event descriptions must not be empty.")
            .Must(descriptions => descriptions.ContainsKey("pl"))
            .WithMessage("Polish event description must be provided.");

        RuleForEach(x => x.EventDates)
            .Must(x => x.StartDate > SystemClock.UtcNow.ToUnixTimeSeconds())
            .WithMessage("Event start date must be greater than the current date and time.")
            .Must(x => x.EndDate > x.StartDate).WithMessage("Event end date must be greater than start date.");

        RuleFor(x => x.EventHostName)
            .MinimumLength(3).WithMessage("Event host name must be at least 3 characters long.")
            .MaximumLength(1000).WithMessage("Event host name must not be longer than 1000 characters.");

        RuleFor(x => x.EventHostUrl)
            .MinimumLength(12).WithMessage("Event host URL must be at least 12 characters long.")
            .MaximumLength(1000).WithMessage("Event host URL must not be longer than 1000 characters.");

        RuleFor(x => x.EventImageUrl)
            .MinimumLength(12).WithMessage("Event image URL must be at least 12 characters long.")
            .MaximumLength(1000).WithMessage("Event image URL must not be longer than 1000 characters.");

        RuleFor(x => x.EventUrl)
            .MinimumLength(12).WithMessage("Event URL must be at least 12 characters long.")
            .MaximumLength(1000).WithMessage("Event URL must not be longer than 1000 characters.");

        RuleFor(x => x.EventLocationCoordinatesLatitude)
            .InclusiveBetween(-90, 90).WithMessage("Event latitude must be between -90 and 90.");

        RuleFor(x => x.EventLocationCoordinatesLongitude)
            .InclusiveBetween(-180, 180).WithMessage("Event longitude must be between -180 and 180.");

        RuleFor(x => x.EventLocationCity)
            .NotEmpty().WithMessage("Event city must not be empty.")
            .MaximumLength(1000).WithMessage("Event city must not be longer than 1000 characters.");

        RuleFor(x => x.EventLocationProvince)
            .MaximumLength(1000).WithMessage("Event province must not be longer than 1000 characters.");

        RuleFor(x => x.EventLocationPostalCode)
            .MaximumLength(10).WithMessage("Event postal code must not be longer than 10 characters.");

        RuleFor(x => x.EventLocationName)
            .MaximumLength(1000).WithMessage("Event location name must not be longer than 1000 characters.");

        RuleFor(x => x.EventLocationStreetName)
            .MaximumLength(1000).WithMessage("Event street name must not be longer than 1000 characters.");

        RuleFor(x => x.EventLocationStreetNumber)
            .MaximumLength(50).WithMessage("Event street number must not be longer than 50 characters.");

        RuleFor(x => x.EventLocationStreetAdditionalInfo)
            .MaximumLength(1000).WithMessage("Event additional location info must not be longer than 1000 characters.");

        RuleFor(x => x.EventStatus)
            .NotEmpty().WithMessage("Event status must not be empty.")
            .MaximumLength(1000).WithMessage("Event status must not be longer than 1000 characters.");

        RuleFor(x => x.EventEditors)
            .Must(x => x.IsUnique()).WithMessage("Event editors must be unique.");

        RuleFor(x => x.EventEditors)
            .Must((x, y) => !y.Intersect(x.SeriesEditors).Any())
            .WithMessage("Event editors cannot contain series editors values.");
    }
}