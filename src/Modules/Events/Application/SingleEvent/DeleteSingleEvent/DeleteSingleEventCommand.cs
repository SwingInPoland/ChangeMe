using ChangeMe.Modules.Events.Application.Contracts;

namespace ChangeMe.Modules.Events.Application.SingleEvent.DeleteSingleEvent;

public record DeleteSingleEventCommand(Guid SingleEventId) : CommandBase;