using ChangeMe.Shared.Domain;
using ChangeMe.Shared.Extensions;
using FluentValidation;

namespace ChangeMe.Modules.Events.Application.Series.SeriesEvent.ChangeSeriesEventsMainAttributes;

internal class ChangeSeriesEventsMainAttributesCommandValidator : AbstractValidator<ChangeSeriesEventsMainAttributesCommand>
{
    public ChangeSeriesEventsMainAttributesCommandValidator()
    {
        RuleFor(x => x.SeriesId)
            .NotEmpty().WithMessage("Series ID must not be empty.");

        RuleFor(x => x.Names)
            .NotEmpty().WithMessage("Names must not be empty.")
            .Must(names => names.ContainsKey("pl")).WithMessage("Polish name must be provided.");

        RuleFor(x => x.Descriptions)
            .NotEmpty().WithMessage("Descriptions must not be empty.")
            .Must(descriptions => descriptions.ContainsKey("pl"))
            .WithMessage("Polish description must be provided.");

        RuleForEach(x => x.EventData)
            .Must(x => x.StartDate > SystemClock.UtcNow.ToUnixTimeSeconds())
            .WithMessage("Start date must be greater than the current date and time.")
            .Must(x => x.EndDate > x.StartDate)
            .WithMessage("End date must be greater than start date.");

        RuleFor(x => x.EventData.Select(d => d.SeriesEventId).ToArray())
            .Must(x => x.IsUnique()).WithMessage("Editors must be unique.");

        RuleFor(x => x.HostName)
            .MinimumLength(3).WithMessage("Host name must be at least 3 characters long.")
            .MaximumLength(1000).WithMessage("Host name must not be longer than 1000 characters.");

        RuleFor(x => x.HostUrl)
            .MinimumLength(12).WithMessage("Host URL must be at least 12 characters long.")
            .MaximumLength(1000).WithMessage("Host URL must not be longer than 1000 characters.");

        RuleFor(x => x.ImageUrl)
            .MinimumLength(12).WithMessage("Image URL must be at least 12 characters long.")
            .MaximumLength(1000).WithMessage("Image URL must not be longer than 1000 characters.");

        RuleFor(x => x.EventUrl)
            .MinimumLength(12).WithMessage("Event URL must be at least 12 characters long.")
            .MaximumLength(1000).WithMessage("Event URL must not be longer than 1000 characters.");

        RuleFor(x => x.LocationCoordinatesLatitude)
            .InclusiveBetween(-90, 90).WithMessage("Latitude must be between -90 and 90.");

        RuleFor(x => x.LocationCoordinatesLongitude)
            .InclusiveBetween(-180, 180).WithMessage("Longitude must be between -180 and 180.");

        RuleFor(x => x.LocationCity)
            .NotEmpty().WithMessage("City must not be empty.")
            .MaximumLength(1000).WithMessage("City must not be longer than 1000 characters.");

        RuleFor(x => x.LocationProvince)
            .MaximumLength(1000).WithMessage("Province must not be longer than 1000 characters.");

        RuleFor(x => x.LocationPostalCode)
            .MaximumLength(10).WithMessage("Postal code must not be longer than 10 characters.");

        RuleFor(x => x.LocationName)
            .MaximumLength(1000).WithMessage("Location name must not be longer than 1000 characters.");

        RuleFor(x => x.LocationStreetName)
            .MaximumLength(1000).WithMessage("Street name must not be longer than 1000 characters.");

        RuleFor(x => x.LocationStreetNumber)
            .MaximumLength(50).WithMessage("Street number must not be longer than 50 characters.");

        RuleFor(x => x.LocationStreetAdditionalInfo)
            .MaximumLength(1000).WithMessage("Additional location info must not be longer than 1000 characters.");
    }
}