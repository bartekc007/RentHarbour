using Moq;
using Catalog.Application.Domains.Property.GetRentedProperites;
using Catalog.Persistance.Repositories.MongoDb;
using Catalog.Persistance.Repositories.Psql;
using RentHarbor.MongoDb.Entities;
using Catalog.Persistance.Entities;

namespace Catalog.Unit.Tests.Application.Domains.GetRentedProperties
{
    public class GetRentedPropertiesQueryHandlerTests
    {
        private readonly Mock<IPropertyRepository> _propertyRepositoryMock;
        private readonly Mock<IRentalRepository> _rentalRepositoryMock;
        private readonly GetRentedPropertiesQueryHandler _handler;

        public GetRentedPropertiesQueryHandlerTests()
        {
            _propertyRepositoryMock = new Mock<IPropertyRepository>();
            _rentalRepositoryMock = new Mock<IRentalRepository>();
            _handler = new GetRentedPropertiesQueryHandler(_propertyRepositoryMock.Object, _rentalRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnRentedProperties_WhenPropertiesExist()
        {
            // Arrange
            var query = new GetRentedPropertiesQuery { UserId = "user1" };
            var rentals = new List<Rental>
        {
            new Rental { PropertyId = "1", StartDate = new DateTime(2022, 1, 1), EndDate = new DateTime(2022, 12, 31) },
            new Rental { PropertyId = "2", StartDate = new DateTime(2022, 6, 1), EndDate = new DateTime(2022, 12, 31) }
        };
            var properties = new List<Property>
        {
            new Property { Id = "1", Name = "Property1", Address = new Address { Street = "Street1" } },
            new Property { Id = "2", Name = "Property2", Address = new Address { Street = "Street2" } }
        };

            _rentalRepositoryMock.Setup(repo => repo.GetRentalsByUserIdAsync(query.UserId)).ReturnsAsync(rentals);
            _propertyRepositoryMock.Setup(repo => repo.GetPropertyByIdAsync("1")).ReturnsAsync(properties[0]);
            _propertyRepositoryMock.Setup(repo => repo.GetPropertyByIdAsync("2")).ReturnsAsync(properties[1]);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result.Data);
            Assert.Equal(2, result.Data.Count);
            Assert.Equal("Property1", result.Data[0].PropertyName);
            Assert.Equal("Street1", result.Data[0].PropertyStreet);
            Assert.Equal("Property2", result.Data[1].PropertyName);
            Assert.Equal("Street2", result.Data[1].PropertyStreet);
        }

        [Fact]
        public async Task Handle_ShouldReturnEmptyList_WhenNoRentedPropertiesExist()
        {
            // Arrange
            var query = new GetRentedPropertiesQuery { UserId = "user1" };
            var rentals = new List<Rental>();

            _rentalRepositoryMock.Setup(repo => repo.GetRentalsByUserIdAsync(query.UserId)).ReturnsAsync(rentals);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result.Data);
            Assert.Empty(result.Data);
        }
    }
}


