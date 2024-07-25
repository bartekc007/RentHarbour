using AutoMapper;
using Document.Application.Domain.Document.Common.Dto;
using Document.Persistance.Repositories.Mongo;
using Document.Persistance.Repositories.Psql;
using MediatR;
using MongoDB.Bson;
using RentHarbor.MongoDb.Entities;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Document.Application.Domain.Document.AddDocument
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

                await _documentRepository.MarkDocumentsAsNotLatestAsync(request.OfferId);
                var offer = await _rentalRequestRepository.GetRentalRequestByOfferIdAsync(request.OfferId);

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
