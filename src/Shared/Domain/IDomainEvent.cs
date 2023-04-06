using Mediator;

namespace ChangeMe.Shared.Domain;

public interface IDomainEvent : INotification
{
    Guid Id { get; }
    DateTimeOffset OccurredOn { get; }
}