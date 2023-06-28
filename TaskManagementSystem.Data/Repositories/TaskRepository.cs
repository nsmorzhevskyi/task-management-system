using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Domain.Entities;
using TaskManagementSystem.Domain.Exceptions;

namespace TaskManagementSystem.Data.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly TaskManagementSystemContext _dbContext;

    public TaskRepository(TaskManagementSystemContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<TaskActivity>> GetAllTasksAsync()
    {
        return await _dbContext.Tasks.AsNoTracking().ToListAsync();
    }

    public async Task<TaskActivity> GetTaskByIdAsync(Guid id)
    {
        var entity = await _dbContext.Tasks.FindAsync(id);

        if (entity is null)
        {
            throw new EntityNotFoundException();
        }

        _dbContext.Entry(entity).State = EntityState.Detached;
        return entity;
    }

    public async Task CreateTaskAsync(TaskActivity task)
    {
        _dbContext.Tasks.Add(task);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateTaskAsync(TaskActivity task)
    {
        var entity = await _dbContext.Tasks.FindAsync(task.Id);

        if (entity is null)
        {
            throw new EntityNotFoundException();
        }
        entity.SetName(task.Name);
        entity.SetDescription(task.Description);
        entity.SetAssignedTo(task.AssignedTo);
        entity.SetStatus(task.Status);

        await _dbContext.SaveChangesAsync();
    }
}