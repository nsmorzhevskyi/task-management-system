using System.Text.Json.Serialization;
using TaskStatus = TaskManagementSystem.Domain.Entities.TaskStatus;

namespace TaskManagementSystem.API.Models;

public class UpdateTaskRequestModel
{
    /// <summary>
    /// Gets or sets task Name.
    /// </summary>
    [JsonRequired]
    public string Name { get; set; }
    
    /// <summary>
    /// Gets or sets task Description.
    /// </summary>
    [JsonRequired]
    public string Description { get; set; }
    
    /// <summary>
    /// Gets or sets task AssignedTo property.
    /// </summary>
    public string AssignedTo { get; set; }
    
    /// <summary>
    /// Gets or sets task Status.
    /// </summary>
    [JsonRequired]
    public TaskStatus Status { get; set; }
}