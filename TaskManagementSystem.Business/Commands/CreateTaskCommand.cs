using MediatR;
using TaskManagementSystem.Data.Repositories;
using TaskManagementSystem.Domain.Entities;

namespace TaskManagementSystem.Business.Commands;

public class CreateTaskCommand : IRequestHandler<CreateTaskCommand.Request, CreateTaskCommand.Response>
{
    private readonly ITaskRepository _taskRepository;

    public CreateTaskCommand(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
    {
        var task = TaskActivity.Create(Guid.NewGuid(), request.TaskName, request.TaskDescription, request.AssignedTo);
        await _taskRepository.CreateTaskAsync(task);
        
        return new Response(task);
    }
    
    public class Request : IRequest<Response>
    {
        public string TaskName { get; set; }
        public string TaskDescription { get; set; }
        public string AssignedTo { get; set; }
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