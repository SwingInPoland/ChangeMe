namespace ChangeMe.Modules.Events.Infrastructure.Configuration.Processing.Inbox;

public record InboxMessageDto(Guid Id, string Type, string Data);