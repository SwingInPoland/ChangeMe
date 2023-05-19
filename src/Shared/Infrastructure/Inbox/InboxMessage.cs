namespace ChangeMe.Shared.Infrastructure.Inbox;

public class InboxMessage
{
    public Guid Id { get; set; }
    public DateTimeOffset OccurredOn { get; set; }
    public string Type { get; set; }
    public string Data { get; set; }
    public DateTimeOffset? ProcessedDate { get; set; }

    public InboxMessage(DateTimeOffset occurredOn, string type, string data)
    {
        Id = Guid.NewGuid();
        OccurredOn = occurredOn;
        Type = type;
        Data = data;
    }

    /// <summary>
    /// EF Core
    /// </summary>
    private InboxMessage() { }
}