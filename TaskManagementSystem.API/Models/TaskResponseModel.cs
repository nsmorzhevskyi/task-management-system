﻿using TaskStatus = TaskManagementSystem.Domain.Entities.TaskStatus;

namespace TaskManagementSystem.API.Models;

public class TaskResponseModel
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string AssignedTo { get; set; }
    public TaskStatus Status { get; set; }
}