namespace ChangeMe.Shared.Infrastructure.DomainEventsDispatching.DomainNotificationsMapper;

public interface IDomainNotificationsMapper
{
    string? GetName(Type type);

    Type? GetType(string name);
}