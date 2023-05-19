using ChangeMe.Modules.Events.Application.Contracts;

namespace ChangeMe.Modules.Events.Infrastructure.Configuration.Processing.Inbox;

public record ProcessInboxCommand : CommandBase, IRecurringCommand;