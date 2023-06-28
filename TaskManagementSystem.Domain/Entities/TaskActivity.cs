using TaskManagementSystem.Domain.Exceptions;

namespace TaskManagementSystem.Domain.Entities;

// To resolve conflict with .NET Task class I would adjust domain name "Task" a bit to have a clear visible difference
public class TaskActivity : BaseEntity
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string AssignedTo { get; private set; }
    public TaskStatus Status { get; private set; }

    private TaskActivity(Guid id, string name, string description, string assignedTo)
    {
        Id = id;
        Name = name;
        Description = description;
        AssignedTo = assignedTo;
        Status = TaskStatus.NotStarted;
    }
    
    // Example. Validation rules depend on business requirement.
    public static TaskActivity Create(Guid id, string name, string description, string assignedTo)
    {
        if (id == Guid.Empty)
            throw new DomainException("First name must be between 2 and 50 characters.");
        
        if (string.IsNullOrWhiteSpace(name) || name.Length < 2 || name.Length > 50)
            throw new DomainException("First name must be between 2 and 50 characters.");

        if (string.IsNullOrWhiteSpace(description) || description.Length < 3 || description.Length > 200)
            throw new DomainException("Last name must be between 3 and 200 characters.");

        var task = new TaskActivity(id, name, description, assignedTo);
        return task;
    }

    public void SetName(string name)
    {
        if (!string.IsNullOrWhiteSpace(name))
        {
            Name = name;
        }
    }
    
    public void SetDescription(string description)
    {
        if (!string.IsNullOrWhiteSpace(description))
        {
            Name = description;
        }
    }
    
    public void SetAssignedTo(string assignedTo)
    {
        if (!string.IsNullOrWhiteSpace(assignedTo))
        {
            Name = assignedTo;
        }
    }

    public void SetStatus(TaskStatus status)
    {
        if (status != TaskStatus.Default)
        {
            Status = status;
        }
    }
}