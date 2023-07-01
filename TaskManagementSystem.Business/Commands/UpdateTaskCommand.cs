using MediatR;
using TaskManagementSystem.Business.Events;
using TaskManagementSystem.Data.Repositories;
using TaskManagementSystem.Domain.Entities;
using TaskManagementSystem.Infrastructure.Interfaces;
using TaskStatus = TaskManagementSystem.Domain.Entities.TaskStatus;

namespace TaskManagementSystem.Business.Commands;

public class UpdateTaskCommand : IRequestHandler<UpdateTaskCommand.Request, UpdateTaskCommand.Response>
{
    private readonly ITaskRepository _taskRepository;
    private readonly IEventBus _eventBus;

    public UpdateTaskCommand(ITaskRepository taskRepository, IEventBus eventBus)
    {
        _taskRepository = taskRepository;
        _eventBus = eventBus;
    }

    public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
    {
        var task = TaskActivity.Create(request.Id, request.TaskName, request.TaskDescription, request.AssignedTo);
        task.SetStatus(request.Status);
        
        await _taskRepository.UpdateTaskAsync(task);

        await _eventBus.PublishAsync(new TaskUpdatedEvent<TaskActivity>
        {
            Data = task,
            Name = "TaskManagementSystem.TaskUpdatedEvent"
        }, cancellationToken);
        
        return new Response(task);
    }
    
    public class Request : IRequest<Response>
    {
        public Guid Id { get; set; }
        public string TaskName { get; set; }
        public string TaskDescription { get; set; }
        public string AssignedTo { get; set; }
        public TaskStatus Status { get; set; }
    }

    public class Response
    {
        public Response(TaskActivity task)
        {
            Task = task;
        }
        
        public TaskActivity Task { get; private set; }
    }
}