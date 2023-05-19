using ChangeMe.Modules.Events.Application.Contracts;

namespace ChangeMe.Modules.Events.Application.SingleEvent.ChangeSingleEventEditors;

public record ChangeSingleEventEditorsCommand(Guid SingleEventId, string[] UserIds) : CommandBase;