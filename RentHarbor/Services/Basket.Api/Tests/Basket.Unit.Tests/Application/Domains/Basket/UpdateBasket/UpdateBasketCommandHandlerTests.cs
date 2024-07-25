using Moq;
using Basket.Persistance.Repositories;
using Basket.Application.Domains.Basket.UpdateBasket;
using RentHarbor.MongoDb.Entities;


namespace Basket.Unit.Tests.Application.Domains.Basket.UpdateBasket
{
    public class UpdateBasketCommandHandlerTests
    {
        private readonly Mock<IPropertyRepository> _propertyRepositoryMock;
        private readonly UpdateBasketCommandHandler _handler;

        public UpdateBasketCommandHandlerTests()
        {
            _propertyRepositoryMock = new Mock<IPropertyRepository>();
            _handler = new UpdateBasketCommandHandler(_propertyRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccessResult_WhenPropertyIsAdded()
        {
            var command = new UpdateBasketCommand
            {
                UserId = "test_user",
                PropertyId = "test_property",
                Action = BasketAction.Add,
                Name = "Test Property",
                Description = "Test Description",
                AddressStreet = "Test Street",
                AddressCity = "Test City",
                AddressState = "Test State",
                AddressPostalCode = "12345",
                AddressCountry = "Test Country",
                Price = 100.0,
                Bedrooms = 2,
                Bathrooms = 1,
                AreaSquareMeters = 50,
                IsAvailable = true
            };

            _propertyRepositoryMock.Setup(repo => repo.AddPropertyAsync(It.IsAny<Property>()))
                .Returns(Task.CompletedTask);

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.True(result.Success);
            Assert.Null(result.ErrorMessage);
            _propertyRepositoryMock.Verify(repo => repo.AddPropertyAsync(It.IsAny<Property>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailureResult_WhenExceptionIsThrown()
        {
            var command = new UpdateBasketCommand
            {
                UserId = "test_user",
                PropertyId = "test_property",
                Action = BasketAction.Add
            };

            _propertyRepositoryMock.Setup(repo => repo.AddPropertyAsync(It.IsAny<Property>()))
                .ThrowsAsync(new Exception("Database error"));

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.False(result.Success);
            Assert.Equal("Database error", result.ErrorMessage);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccessResult_WhenPropertyIsUpdated()
        {
            var command = new UpdateBasketCommand
            {
                UserId = "test_user",
                PropertyId = "test_property",
                Action = BasketAction.Update,
                Name = "Updated Property",
                Description = "Updated Description",
                AddressStreet = "Updated Street",
                AddressCity = "Updated City",
                AddressState = "Updated State",
                AddressPostalCode = "54321",
                AddressCountry = "Updated Country",
                Price = 200.0,
                Bedrooms = 3,
                Bathrooms = 2,
                AreaSquareMeters = 100,
                IsAvailable = false
            };

            var property = new Property
            {
                UserId = "test_user",
                Id = "test_property"
            };

            _propertyRepositoryMock.Setup(repo => repo.UpdatePropertyAsync(It.IsAny<Property>()))
                .ReturnsAsync(true);

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.True(result.Success);
            Assert.Null(result.ErrorMessage);
            _propertyRepositoryMock.Verify(repo => repo.UpdatePropertyAsync(It.IsAny<Property>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccessResult_WhenPropertyIsRemoved()
        {
            var command = new UpdateBasketCommand
            {
                UserId = "test_user",
                PropertyId = "test_property",
                Action = BasketAction.Remove
            };

            _propertyRepositoryMock.Setup(repo => repo.RemovePropertyAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(true);

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.True(result.Success);
            Assert.Null(result.ErrorMessage);
            _propertyRepositoryMock.Verify(repo => repo.RemovePropertyAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }
    }
}

