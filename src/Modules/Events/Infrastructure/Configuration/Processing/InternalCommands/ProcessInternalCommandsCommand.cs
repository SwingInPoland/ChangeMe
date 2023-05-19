using ChangeMe.Modules.Events.Application.Contracts;

namespace ChangeMe.Modules.Events.Infrastructure.Configuration.Processing.InternalCommands;

// public instead of internal cause of Mediator
public record ProcessInternalCommandsCommand : CommandBase, IRecurringCommand;