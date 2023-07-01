using AutoFixture;
using FluentAssertions;
using Moq;
using TaskManagementSystem.Business.Commands;
using TaskManagementSystem.Business.Events;
using TaskManagementSystem.Data.Repositories;
using TaskManagementSystem.Domain.Entities;
using TaskManagementSystem.Infrastructure.Interfaces;
using Xunit;

namespace TaskManagementSystem.Business.Tests;

public class UpdateTaskCommandTests
{
    private readonly Fixture _fixture;
    private readonly Mock<ITaskRepository> _taskRepositoryMock;
    private readonly Mock<IEventBus> _eventBusMock;
    private readonly UpdateTaskCommand _command;

    public UpdateTaskCommandTests()
    {
        _fixture = new Fixture();
        _taskRepositoryMock = new Mock<ITaskRepository>();
        _eventBusMock = new Mock<IEventBus>();

        _command = new UpdateTaskCommand(_taskRepositoryMock.Object, _eventBusMock.Object);
    }

    [Fact]
    public async Task Handle_ValidRequest_TaskUpdatedSuccessfully()
    {
        // Arrange
        var request = _fixture.Create<UpdateTaskCommand.Request>();
        _taskRepositoryMock.Setup(x => x.UpdateTaskAsync(It.Is<TaskActivity>(t => t.Id == request.Id)));
        _eventBusMock.Setup(x => x.PublishAsync(It.IsAny<TaskUpdatedEvent<TaskActivity>>(), CancellationToken.None));

        // Act
        var response = await _command.Handle(request, CancellationToken.None);
        
        // Assert
        response.Task.Should().NotBeNull();
        response.Task.Id.Should().Be(request.Id);
        response.Task.Name.Should().Be(request.TaskName);
        response.Task.Description.Should().Be(request.TaskDescription);
        response.Task.AssignedTo.Should().Be(request.AssignedTo);
        
        _taskRepositoryMock.Verify(x => x.UpdateTaskAsync(It.IsAny<TaskActivity>()), Times.Once);
        _eventBusMock.Verify(x => x.PublishAsync(It.IsAny<TaskUpdatedEvent<TaskActivity>>(),CancellationToken.None), Times.Once);
    }
}