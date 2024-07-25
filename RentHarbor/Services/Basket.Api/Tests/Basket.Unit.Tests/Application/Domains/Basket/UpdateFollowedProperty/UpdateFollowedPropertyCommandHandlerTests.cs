using Moq;
using Basket.Persistance.Repositories;
using Basket.Application.Domains.Basket.UpdateFollowedProperty;
using RentHarbor.MongoDb.Entities;

namespace Basket.Unit.Tests.Application.Domains.Basket.UpdateFollowedProperty
{
    public class UpdateFollowedPropertyCommandHandlerTests
    {
        private readonly Mock<IPropertyRepository> _propertyRepositoryMock;
        private readonly UpdateFollowedPropertyCommandHandler _handler;

        public UpdateFollowedPropertyCommandHandlerTests()
        {
            _propertyRepositoryMock = new Mock<IPropertyRepository>();
            _handler = new UpdateFollowedPropertyCommandHandler(_propertyRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccessResult_WhenPropertyIsAdded()
        {
            var command = new UpdateFollowedPropertyCommand
            {
                Id = "00000000-0000-0000-0000-000000000000",
                UserId = "00000000-0000-0000-0000-000000000000",
                PropertyId = "00000000-0000-0000-0000-000000000000",
                Action = UserPropertyAction.Add
            };

            _propertyRepositoryMock.Setup(repo => repo.AddUserPropertyAsync(It.IsAny<UserProperty>()))
                .Returns(Task.CompletedTask);

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.True(result.Success);
            Assert.Null(result.ErrorMessage);
            _propertyRepositoryMock.Verify(repo => repo.AddUserPropertyAsync(It.IsAny<UserProperty>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailureResult_WhenExceptionIsThrown()
        {
            var command = new UpdateFollowedPropertyCommand
            {
                Id = "00000000-0000-0000-0000-000000000000",
                UserId = "00000000-0000-0000-0000-000000000000",
                PropertyId = "00000000-0000-0000-0000-000000000000",
                Action = UserPropertyAction.Add
            };

            _propertyRepositoryMock.Setup(repo => repo.AddUserPropertyAsync(It.IsAny<UserProperty>()))
                .ThrowsAsync(new Exception("Database error"));

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.False(result.Success);
            Assert.Equal("Database error", result.ErrorMessage);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccessResult_WhenPropertyIsRemoved()
        {
            var command = new UpdateFollowedPropertyCommand
            {
                Id = "00000000-0000-0000-0000-000000000000",
                UserId = "00000000-0000-0000-0000-000000000000",
                PropertyId = "00000000-0000-0000-0000-000000000000",
                Action = UserPropertyAction.Remove
            };

            _propertyRepositoryMock.Setup(repo => repo.RemoveUserPropertyAsync(command.Id, command.UserId))
                .ReturnsAsync(true);

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.True(result.Success);
            Assert.Null(result.ErrorMessage);
            _propertyRepositoryMock.Verify(repo => repo.RemoveUserPropertyAsync(command.Id, command.UserId), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailureResult_WhenPropertyRemovalFails()
        {
            var command = new UpdateFollowedPropertyCommand
            {
                Id = "00000000-0000-0000-0000-000000000000",
                UserId = "00000000-0000-0000-0000-000000000000",
                PropertyId = "00000000-0000-0000-0000-000000000000",
                Action = UserPropertyAction.Remove
            };

            _propertyRepositoryMock.Setup(repo => repo.RemoveUserPropertyAsync(command.Id, command.UserId))
                .ReturnsAsync(false);

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.False(result.Success);
            Assert.Null(result.ErrorMessage);
            _propertyRepositoryMock.Verify(repo => repo.RemoveUserPropertyAsync(command.Id, command.UserId), Times.Once);
        }
    }
}

