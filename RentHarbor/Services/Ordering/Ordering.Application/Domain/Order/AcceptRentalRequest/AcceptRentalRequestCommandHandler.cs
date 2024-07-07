using MediatR;
using Ordering.Persistance.Repositories.Psql;

namespace Ordering.Application.Domain.Order.AcceptRentalRequest
{
    public class AcceptRentalRequestCommandHandler : IRequestHandler<AcceptRentalRequestCommand, bool>
    {
        private readonly IRentalRequestRepository _rentalRepository;
        public AcceptRentalRequestCommandHandler(IRentalRequestRepository rentalRepository)
        {
            _rentalRepository = rentalRepository;
        }
        public async Task<bool> Handle(AcceptRentalRequestCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var rentalOffer = await _rentalRepository.GetRentalRequestByOfferIdAsync(request.OfferId);
                if (request.Status == RentalRequestStatus.Accept)
                {
                    if (rentalOffer.TenantId == request.UserId)
                    {
                        rentalOffer.OwnerAcceptance = true;
                        await _rentalRepository.ModifyAsync(rentalOffer);
                    }
                    else
                    {
                        rentalOffer.UserAcceptance = true;
                        await _rentalRepository.ModifyAsync(rentalOffer);
                    }
                }
                if (request.Status == RentalRequestStatus.Modify)
                {
                    if (rentalOffer.TenantId == request.UserId)
                    {
                        rentalOffer.UserAcceptance = false;
                        await _rentalRepository.ModifyAsync(rentalOffer);
                    }
                    else
                    {
                        rentalOffer.OwnerAcceptance = false;
                        await _rentalRepository.ModifyAsync(rentalOffer);
                    }
                }
                if (request.Status == RentalRequestStatus.Close)
                {
                    await _rentalRepository.DeleteAsync(rentalOffer.Id);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            
        }
    }
}
