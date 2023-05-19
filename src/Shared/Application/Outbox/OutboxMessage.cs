namespace ChangeMe.Shared.Application.Outbox;

public class OutboxMessage
{
    public Guid Id { get; set; }

    public DateTimeOffset OccurredOn { get; set; }

    public string Type { get; set; }

    public string Data { get; set; }

    public DateTimeOffset? ProcessedDate { get; set; }

    public OutboxMessage(Guid id, DateTimeOffset occurredOn, string type, string data)
    {
        Id = id;
        OccurredOn = occurredOn;
        Type = type;
        Data = data;
    }

    /// <summary>
    /// EF Core
    /// </summary>
    private OutboxMessage() { }
}