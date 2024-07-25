using Moq;
using RentHarbor.MongoDb.Entities;
using Ordering.Persistance.Repositories.Psql;
using Ordering.Persistance.Repositories.Mongo;
using Ordering.Application.Domain.Order.CreateRentalRequest;
using Ordering.Persistance.Entities;

namespace Ordering.Unit.Tests.Application.Domain.Order.CreateRentalRequest
{
    public class CreateRentalRequestCommandHandlerTests
    {
        private readonly Mock<IRentalRequestRepository> _rentalRequestRepositoryMock;
        private readonly Mock<IPropertyRepository> _propertyRepositoryMock;
        private readonly CreateRentalRequestCommandHandler _handler;

        public CreateRentalRequestCommandHandlerTests()
        {
            _rentalRequestRepositoryMock = new Mock<IRentalRequestRepository>();
            _propertyRepositoryMock = new Mock<IPropertyRepository>();
            _handler = new CreateRentalRequestCommandHandler(_rentalRequestRepositoryMock.Object, _propertyRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccessResult_WhenRequestIsValid()
        {
            var request = new CreateRentalRequestCommand
            {
                PropertyId = Guid.NewGuid().ToString(),
                UserId = "user1",
                StartDate = DateTime.Now.AddDays(1),
                EndDate = DateTime.Now.AddDays(2)
            };

            _propertyRepositoryMock.Setup(repo => repo.GetOwnerIdByPropertyIdAsync(It.IsAny<string>())).ReturnsAsync("user1");
            _rentalRequestRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<RentalRequest>())).ReturnsAsync(1);

            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.NotEmpty(result.Status);
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenUserNotFound()
        {
            var request = new CreateRentalRequestCommand
            {
                PropertyId = Guid.NewGuid().ToString(),
                UserId = "user1",
                StartDate = DateTime.Now.AddDays(1),
                EndDate = DateTime.Now.AddDays(2)
            };

            var property = new Property { Id = Guid.NewGuid().ToString(), Name = "Property1" };

            _propertyRepositoryMock.Setup(repo => repo.GetOwnerIdByPropertyIdAsync(It.IsAny<string>())).ReturnsAsync("");

            await Assert.ThrowsAsync<Exception>(async () => await _handler.Handle(request, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ShouldReturnFalse_WhenAddingRentalRequestFails()
        {
            var request = new CreateRentalRequestCommand
            {
                PropertyId = Guid.NewGuid().ToString(),
                UserId = "user1",
                StartDate = DateTime.Now.AddDays(1),
                EndDate = DateTime.Now.AddDays(2)
            };

            _propertyRepositoryMock.Setup(repo => repo.GetOwnerIdByPropertyIdAsync(It.IsAny<string>())).ReturnsAsync("user1");
            _rentalRequestRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<RentalRequest>())).ReturnsAsync(1);

            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.NotEmpty(result.Status);
        }
    }
}

