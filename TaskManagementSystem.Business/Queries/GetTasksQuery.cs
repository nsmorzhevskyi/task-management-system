using MediatR;
using TaskManagementSystem.Data.Repositories;
using TaskManagementSystem.Domain.Entities;

namespace TaskManagementSystem.Business.Queries;

public class GetTasksQuery : IRequestHandler<GetTasksQuery.Request, GetTasksQuery.Response>
{
    private readonly ITaskRepository _taskRepository;

    public GetTasksQuery(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
    {
        var tasks = await _taskRepository.GetAllTasksAsync();
        return new Response(tasks);
    }
    
    public class Request : IRequest<Response>
    {
    }

    public class Response
    {
        public Response(IEnumerable<TaskActivity> tasks)
        {
            Tasks = tasks;
        }
        
        public IEnumerable<TaskActivity> Tasks { get; private set; }
    }
}