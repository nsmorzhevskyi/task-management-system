using MassTransit;
using Microsoft.Extensions.Logging;
using TaskManagementSystem.Data.Repositories;
using TaskManagementSystem.Domain.Entities;

namespace TaskManagementSystem.Business.Events;

public sealed class TaskUpdatedEventConsumer : IConsumer<TaskUpdatedEvent<TaskActivity>>
{
    private readonly ITaskRepository _taskRepository;
    private readonly ILogger<TaskUpdatedEventConsumer> _logger;

    public TaskUpdatedEventConsumer(ITaskRepository taskRepository, ILogger<TaskUpdatedEventConsumer> logger)
    {
        _taskRepository = taskRepository;
        _logger = logger;
    }

    public Task Consume(ConsumeContext<TaskUpdatedEvent<TaskActivity>> context)
    {
        var task = context.Message.Data;
        _taskRepository.UpdateTaskAsync(task);
        
        _logger.LogInformation("Task with id {TaskId} was updated successfully", task.Id);

        return Task.CompletedTask;
    }
}