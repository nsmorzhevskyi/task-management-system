using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Domain.Entities;

namespace TaskManagementSystem.Data;

public class TaskManagementSystemContext : DbContext
{
    public TaskManagementSystemContext(DbContextOptions<TaskManagementSystemContext> options) 
        : base(options)
    {
    }

    public DbSet<TaskActivity> Tasks { get; set; }
}