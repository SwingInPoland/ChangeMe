using ChangeMe.Modules.Events.Application.Contracts;

namespace ChangeMe.Modules.Events.Infrastructure.Configuration.Processing.Outbox;

public record ProcessOutboxCommand : CommandBase, IRecurringCommand;