using ChangeMe.Shared.Domain;
using FluentValidation;

namespace ChangeMe.Modules.Events.Application.SingleEvent.ChangeSingleEventMainAttributes;

internal class ChangeSingleEventMainAttributesCommandValidator : AbstractValidator<ChangeSingleEventMainAttributesCommand>
{
    public ChangeSingleEventMainAttributesCommandValidator()
    {
        RuleFor(x => x.SingleEventId)
            .NotEmpty().WithMessage("Single event ID must not be empty.");

        RuleFor(x => x.Names)
            .NotEmpty().WithMessage("Event names must not be empty.")
            .Must(names => names.ContainsKey("pl")).WithMessage("Polish event name must be provided.");

        RuleFor(x => x.Descriptions)
            .NotEmpty().WithMessage("Event descriptions must not be empty.")
            .Must(descriptions => descriptions.ContainsKey("pl"))
            .WithMessage("Polish event description must be provided.");

        RuleFor(x => x.StartDate)
            .GreaterThan(SystemClock.UtcNow.ToUnixTimeSeconds())
            .WithMessage("Start date must be greater than the current date and time.");

        RuleFor(x => x.EndDate)
            .GreaterThan(x => x.StartDate)
            .WithMessage("End date must be greater than start date.");

        RuleFor(x => x.HostName)
            .MinimumLength(3).WithMessage("Event host name must be at least 3 characters long.")
            .MaximumLength(1000).WithMessage("Event host name must not be longer than 1000 characters.");

        RuleFor(x => x.HostUrl)
            .MinimumLength(12).WithMessage("Event host URL must be at least 12 characters long.")
            .MaximumLength(1000).WithMessage("Event host URL must not be longer than 1000 characters.");

        RuleFor(x => x.ImageUrl)
            .MinimumLength(12).WithMessage("Event image URL must be at least 12 characters long.")
            .MaximumLength(1000).WithMessage("Event image URL must not be longer than 1000 characters.");

        RuleFor(x => x.EventUrl)
            .MinimumLength(12).WithMessage("Event URL must be at least 12 characters long.")
            .MaximumLength(1000).WithMessage("Event URL must not be longer than 1000 characters.");

        RuleFor(x => x.LocationCoordinatesLatitude)
            .InclusiveBetween(-90, 90).WithMessage("Event latitude must be between -90 and 90.");

        RuleFor(x => x.LocationCoordinatesLongitude)
            .InclusiveBetween(-180, 180).WithMessage("Event longitude must be between -180 and 180.");

        RuleFor(x => x.LocationCity)
            .NotEmpty().WithMessage("Event city must not be empty.")
            .MaximumLength(1000).WithMessage("Event city must not be longer than 1000 characters.");

        RuleFor(x => x.LocationProvince)
            .MaximumLength(1000).WithMessage("Event province must not be longer than 1000 characters.");
    }
}