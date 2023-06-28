using System.Text.Json.Serialization;

namespace TaskManagementSystem.API.Models;

/// <summary>
/// Create task request model.
/// </summary>
public class CreateTaskRequestModel
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
}