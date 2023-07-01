namespace TaskManagementSystem.Infrastructure.Interfaces;

public interface IEventBus
{
    public Task PublishAsync<T>(T message, CancellationToken cancellationToken = default)
        where T : class;
}