using ChangeMe.Shared.Infrastructure.EventBus;

namespace ChangeMe.Modules.Events.IntegrationEvents;

public record SampleIntegrationEvent(Guid Id, DateTimeOffset OccurredOn, Guid SomeProperty)
    : IntegrationEvent(Id, OccurredOn);