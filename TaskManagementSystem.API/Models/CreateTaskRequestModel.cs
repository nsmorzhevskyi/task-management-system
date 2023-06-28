namespace TaskManagementSystem.API.Models;

public class CreateTaskRequestModel
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string AssignedTo { get; set; }
}