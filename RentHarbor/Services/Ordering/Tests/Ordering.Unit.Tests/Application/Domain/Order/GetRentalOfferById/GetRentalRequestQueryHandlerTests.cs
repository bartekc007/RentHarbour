using Moq;
using RentHarbor.MongoDb.Entities;
using Ordering.Persistance.Repositories.Psql;
using Ordering.Persistance.Repositories.Mongo;
using Ordering.Application.Domain.Order.GetRentalOfferById;
using Ordering.Persistance.Entities;

namespace Ordering.Unit.Tests.Application.Domain.Order.GetRentalOfferById
{
    public class GetRentalRequestQueryHandlerTests
    {
        private readonly Mock<IRentalRequestRepository> _rentalRequestRepositoryMock;
        private readonly Mock<IPropertyRepository> _propertyRepositoryMock;
        private readonly GetRentalRequestQueryHandler _handler;

        public GetRentalRequestQueryHandlerTests()
        {
            _rentalRequestRepositoryMock = new Mock<IRentalRequestRepository>();
            _propertyRepositoryMock = new Mock<IPropertyRepository>();
            _handler = new GetRentalRequestQueryHandler(_rentalRequestRepositoryMock.Object, _propertyRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnRentalRequestResult_WhenRentalRequestExists()
        {
            var request = new GetRentalRequestQuery { OfferId = 1 };
            var rentalRequest = new RentalRequest
            {
                Id = 1,
                PropertyId = "123",
                UserId = "user1",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(1),
                Status = "Pending"
            };
            var property = new Property { Id = "123", Name = "Property1" };

            _rentalRequestRepositoryMock.Setup(repo => repo.GetRentalRequestByOwnerIdAndOfferIdAsync(It.IsAny<string>(), It.IsAny<int>()))
                .ReturnsAsync(rentalRequest);
            _propertyRepositoryMock.Setup(repo => repo.GetOwnerPropertyAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(property);

            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal(1, result.Data.Id);
            Assert.Equal("123", result.Data.PropertyId);
        }
    }
}

