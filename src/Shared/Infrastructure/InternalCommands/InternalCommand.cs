namespace ChangeMe.Shared.Infrastructure.InternalCommands;

public class InternalCommand
{
    public Guid Id { get; set; }
    public string Type { get; set; }
    public string Data { get; set; }
    public DateTimeOffset? ProcessedDate { get; set; }
}