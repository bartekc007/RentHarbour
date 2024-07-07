using AutoMapper;
using MediatR;
using MongoDB.Bson;
using Ordering.Application.Domain.Document.Common.Dto;
using Ordering.Persistance.Repositories.Mongo;
using Ordering.Persistance.Repositories.Psql;
using RentHarbor.MongoDb.Entities;

namespace Ordering.Application.Domain.Document.AddDocument
{
    public class AddDocumentCommandHandler : IRequestHandler<AddDocumentCommand, OfferDocumentDto>
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly IRentalRequestRepository _rentalRequestRepository;
        private readonly IMapper _mapper;

        public AddDocumentCommandHandler(IDocumentRepository documentRepository, IRentalRequestRepository rentalRequestRepository, IMapper mapper)
        {
            _documentRepository = documentRepository;
            _rentalRequestRepository = rentalRequestRepository;
            _mapper = mapper;
        }

        public async Task<OfferDocumentDto> Handle(AddDocumentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request == null)
                    return null;

                if (request.OfferId == null)
                    return null;

                // Przekształć wszystkie istniejące dokumenty na nieaktualne
                await _documentRepository.MarkDocumentsAsNotLatestAsync(request.OfferId);
                var offer = await _rentalRequestRepository.GetRentalRequestByOfferIdAsync(request.OfferId);

                // Odczytaj plik
                byte[] fileContent;
                using (var memoryStream = new MemoryStream())
                {
                    await request.File.CopyToAsync(memoryStream);
                    fileContent = memoryStream.ToArray();
                }

                var document = new OfferDocument
                {
                    DocumentId = ObjectId.GenerateNewId().ToString(),
                    OfferId = request.OfferId,
                    FileName = request.File.FileName,
                    Content = fileContent,
                    UploadDate = DateTime.UtcNow,
                    IsLatest = true
                };

                if (offer.TenantId == request.UserId)
                {
                    document.OwnerId = request.UserId;
                    document.RenterId = offer.UserId;
                }
                else
                {
                    document.RenterId = request.UserId;
                    document.OwnerId = offer.UserId;
                }

                await _documentRepository.AddDocumentAsync(document);

                return _mapper.Map<OfferDocumentDto>(document);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
    }
}
