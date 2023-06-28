using TaskManagementSystem.Domain.Entities;

namespace TaskManagementSystem.Data.Repositories;

public interface ITaskRepository
{
    public Task<IEnumerable<TaskActivity>> GetAllTasksAsync();

    public Task<TaskActivity> GetTaskByIdAsync(Guid id);

    public Task CreateTaskAsync(TaskActivity task);

    public Task UpdateTaskAsync(TaskActivity task);
}