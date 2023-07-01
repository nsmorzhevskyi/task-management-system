using MassTransit;
using Moq;
using TaskManagementSystem.Business.Events;
using TaskManagementSystem.Domain.Entities;
using TaskManagementSystem.Infrastructure.Implementations;
using Xunit;

namespace TaskManagementSystem.Infrastructure.Tests;

public class EventBusTests
{
    [Fact]
    public async Task PublishAsync_ValidMessage_CallsPublishEndpoint()
    {
        // Arrange
        var publishEndpointMock = new Mock<IPublishEndpoint>();
        var eventBus = new EventBus(publishEndpointMock.Object);

        var message = new TaskUpdatedEvent<TaskActivity>();

        // Act
        await eventBus.PublishAsync(message);

        // Assert
        publishEndpointMock.Verify(e => e.Publish(message, CancellationToken.None), Times.Once
        );
    }
}