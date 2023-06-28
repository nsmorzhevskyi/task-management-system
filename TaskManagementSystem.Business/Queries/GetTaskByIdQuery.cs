using MediatR;
using TaskManagementSystem.Data.Repositories;
using TaskManagementSystem.Domain.Entities;

namespace TaskManagementSystem.Business.Queries;

public class GetTaskByIdQuery : IRequestHandler<GetTaskByIdQuery.Request, GetTaskByIdQuery.Response>
{
    private readonly ITaskRepository _taskRepository;

    public GetTaskByIdQuery(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
    {
        var tasks = await _taskRepository.GetTaskByIdAsync(request.Id);
        return new Response(tasks);
    }
    
    public class Request : IRequest<Response>
    {
        public Guid Id { get; set; }
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