namespace ChangeMe.Modules.Events.Infrastructure.Configuration.Processing.Outbox;

public record OutboxMessageDto(Guid Id, string Type, string Data);