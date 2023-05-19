using ChangeMe.Modules.Events.Application.Contracts;

namespace ChangeMe.Modules.Events.Application.SingleEvent.ChangeSingleEventStatus;

public record ChangeSingleEventStatusCommand(Guid SingleEventId, string Status) : CommandBase;