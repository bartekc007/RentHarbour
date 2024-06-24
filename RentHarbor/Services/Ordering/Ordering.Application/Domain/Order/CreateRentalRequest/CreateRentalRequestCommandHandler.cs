using MediatR;
using Ordering.Persistance.Repositories.Psql;
using Ordering.Persistance.Entities;
using Ordering.Persistance.Repositories.Mongo;

namespace Ordering.Application.Domain.Order.CreateRentalRequest
{
    public class CreateRentalRequestCommandHandler : IRequestHandler<CreateRentalRequestCommand, CreateRentalRequestCommandResult>
    {
        private readonly IRentalRequestRepository _rentalRepository;
        private readonly IPropertyRepository _propertyrepository;

        public CreateRentalRequestCommandHandler(IRentalRequestRepository repository, IPropertyRepository propertyrepository)
        {
            _rentalRepository = repository;
            _propertyrepository = propertyrepository;
        }

        public async Task<CreateRentalRequestCommandResult> Handle(CreateRentalRequestCommand request, CancellationToken cancellationToken)
        {
            var OwnerId = await _propertyrepository.GetOwnerIdByPropertyIdAsync(request.PropertyId);

            var rentalRequest = new RentalRequest
            {
                PropertyId = request.PropertyId,
                UserId = request.UserId,
                TenantId = OwnerId,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                Pets = request.Pets,
                NumberOfPeople = request.NumberOfPeople,
                MessageToOwner = request.MessageToOwner,
                Status = "Pending"
            };

            var result = await _rentalRepository.AddAsync(rentalRequest);
            return new CreateRentalRequestCommandResult
            {
                Status = "Pending"
            };
        }
    }
}
