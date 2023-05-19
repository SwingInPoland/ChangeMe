using MediatR;

namespace ChangeMe.Shared.Infrastructure.EventBus;

public abstract record IntegrationEvent(Guid Id, DateTimeOffset OccurredOn) : INotification;