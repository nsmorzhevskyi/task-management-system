namespace TaskManagementSystem.Business.Events;

public record TaskUpdatedEvent<T>
{
    public string Name { get; init; }
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
    public T Data { get; init; }
}