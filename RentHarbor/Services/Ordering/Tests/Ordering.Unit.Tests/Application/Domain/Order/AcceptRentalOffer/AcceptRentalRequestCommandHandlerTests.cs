using Moq;
using Ordering.Application.Domain.Order.AcceptRentalRequest;
using Ordering.Persistance.Entities;
using Ordering.Persistance.Repositories.Mongo;
using Ordering.Persistance.Repositories.Psql;

namespace Ordering.Unit.Tests.Application.Domain.Order.AcceptRentalOffer
{
    public class AcceptRentalRequestCommandHandlerTests
    {
        private readonly Mock<IRentalRequestRepository> _rentalRequestRepositoryMock;
        private readonly Mock<IRentalRepository> _rentalRepositoryMock;
        private readonly Mock<IPropertyRepository> _propertyRepositoryMock;
        private readonly Mock<IPaymentRepository> _paymentRepositoryMock;
        private readonly AcceptRentalRequestCommandHandler _handler;

        public AcceptRentalRequestCommandHandlerTests()
        {
            _rentalRequestRepositoryMock = new Mock<IRentalRequestRepository>();
            _rentalRepositoryMock = new Mock<IRentalRepository>();
            _propertyRepositoryMock = new Mock<IPropertyRepository>();
            _paymentRepositoryMock = new Mock<IPaymentRepository>();
            _handler = new AcceptRentalRequestCommandHandler(_rentalRequestRepositoryMock.Object, _rentalRepositoryMock.Object, _propertyRepositoryMock.Object, _paymentRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnFalse_WhenRentalRequestNotFound()
        {
            var request = new AcceptRentalRequestCommand { Status = RentalRequestStatus.Accept, OfferId = 1, UserId = "user1" };
            _rentalRequestRepositoryMock.Setup(repo => repo.GetRentalRequestByOfferIdAsync(It.IsAny<int>()))
                .ReturnsAsync((RentalRequest)null);

            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.False(result);
        }

        [Fact]
        public async Task Handle_ShouldReturnTrue_WhenRequestIsValid()
        {
            var request = new AcceptRentalRequestCommand { OfferId = 1, UserId = "user1" };
            var rentalRequest = new RentalRequest { Id = 1, UserId = "user1", PropertyId = "property1", StartDate = DateTime.Now, EndDate = DateTime.Now.AddMonths(1) };
            _rentalRequestRepositoryMock.Setup(repo => repo.GetRentalRequestByOfferIdAsync(It.IsAny<int>()))
                .ReturnsAsync(rentalRequest);
            _rentalRequestRepositoryMock.Setup(repo => repo.ModifyAsync(It.IsAny<RentalRequest>()))
                .ReturnsAsync(true);
            _rentalRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Rental>()))
                .ReturnsAsync(1);

            var result = await _handler.Handle(request, CancellationToken.None);

            Assert.True(result);
        }
    }
}

