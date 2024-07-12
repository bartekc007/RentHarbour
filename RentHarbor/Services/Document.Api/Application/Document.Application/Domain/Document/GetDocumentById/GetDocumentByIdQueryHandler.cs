using Document.Application.Domain.Document.Common.Dto;
using Document.Persistance.Repositories.Mongo;
using MediatR;
using MongoDB.Driver;
using RentHarbor.MongoDb.Data;
using RentHarbor.MongoDb.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Document.Application.Domain.Document.GetDocumentById
{
    public class GetDocumentByIdQueryHandler : IRequestHandler<GetDocumentByIdQuery, OfferDocumentDto>
    {
        private readonly IDocumentRepository _documentRepository;

        public GetDocumentByIdQueryHandler(IDocumentRepository documentRepository)
        {
            _documentRepository = documentRepository;
        }

        public async Task<OfferDocumentDto> Handle(GetDocumentByIdQuery request, CancellationToken cancellationToken)
        {
            var document = await _documentRepository.GetDocumentByIdAsync(request.DocumentId);

            if (document == null)
            {
                return null;
            }

            var documentDto = new OfferDocumentDto
            {
                DocumentId = document.DocumentId,
                OfferId = document.OfferId,
                FileName = document.FileName,
                Content = document.Content,
                IsLatest = document.IsLatest
            };

            return documentDto;
        }
    }
}
