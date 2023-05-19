using ChangeMe.Shared.Domain;

namespace ChangeMe.Shared.Application.Events;

public record DomainNotificationBase(Guid Id, IDomainEvent DomainEvent) : IDomainEventNotification;