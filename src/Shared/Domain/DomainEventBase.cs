namespace ChangeMe.Shared.Domain;

//TODO: To record?
public class DomainEventBase : IDomainEvent
{
    public Guid Id { get; }
    public DateTimeOffset OccurredOn { get; }

    public DomainEventBase()
    {
        Id = Guid.NewGuid();
        OccurredOn = SystemClock.UtcNow;
    }
}