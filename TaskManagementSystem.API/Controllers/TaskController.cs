﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.API.Extensions;
using TaskManagementSystem.API.Models;
using TaskManagementSystem.Business.Commands;
using TaskManagementSystem.Business.Queries;

namespace TaskManagementSystem.API.Controllers;

[ApiController]
[Route("api/tasks")]
[AllowAnonymous]
public class TaskController : ControllerBase
{
    private readonly IMediator _mediator;

    public TaskController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Retrieves all the tasks.
    /// </summary>
    /// <returns>Collection of tasks.</returns>
    [HttpGet]
    public async Task<IActionResult> GetAllTasks()
    {
        var response = await _mediator.Send(new GetTasksQuery.Request());
        var mappedTasks = response.Tasks.Select(t => t.MapToResponseModel());

        return Ok(mappedTasks);
    }

    /// <summary>
    /// Gets the task by requested id if one exists.
    /// </summary>
    /// <param name="id">Id.</param>
    /// <returns>Requested task</returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetTaskById(Guid id)
    {
        if (id == Guid.Empty)
        {
            return BadRequest();
        }

        var response = await _mediator.Send(new GetTaskByIdQuery.Request());
        return Ok(response.Task.MapToResponseModel());
    }

    /// <summary>
    /// Creates new task.
    /// </summary>
    /// <param name="requestModel">Create Task request model.</param>
    /// <returns>Created task.</returns>
    [HttpPost]
    public async Task<IActionResult> CreateTask([FromBody] CreateTaskRequestModel requestModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }
        
        var response = await _mediator.Send(new CreateTaskCommand.Request
        {
            TaskName = requestModel.Name,
            TaskDescription = requestModel.Description,
            AssignedTo = requestModel.AssignedTo
        });

        return Accepted(response.Task.MapToResponseModel());
    }

    /// <summary>
    /// Updates the task according to request model.
    /// </summary>
    /// <param name="id">Task Id.</param>
    /// <param name="requestModel">Requested model.</param>
    /// <returns>Updated task entity.</returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTask([FromRoute] Guid id, [FromBody] UpdateTaskRequestModel requestModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }
        
        var response = await _mediator.Send(new UpdateTaskCommand.Request
        {
            Id = id,
            TaskName = requestModel.Name,
            TaskDescription = requestModel.Description,
            AssignedTo = requestModel.AssignedTo,
            Status = requestModel.Status
        });

        return Accepted(response.Task.MapToResponseModel());
    }
}