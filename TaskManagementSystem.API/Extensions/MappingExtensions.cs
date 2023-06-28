using TaskManagementSystem.API.Models;
using TaskManagementSystem.Domain.Entities;

namespace TaskManagementSystem.API.Extensions;

public static class MappingExtensions
{
    public static TaskResponseModel MapToResponseModel(this TaskActivity task)
    {
        return new TaskResponseModel
        {
            Name = task.Name,
            Description = task.Description,
            AssignedTo = task.AssignedTo,
            Status = task.Status.ToString()
        };
    }
}