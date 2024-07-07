using AutoMapper;
using MediatR;
using Ordering.Application.Domain.Document.Common.Dto;
using Ordering.Persistance.Repositories.Mongo;

namespace Ordering.Application.Domain.Document.GetDocumentsByOfferId
{
    public class GetDocumentsByOfferIdHandler : IRequestHandler<GetDocumentsByOfferIdQuery, List<OfferDocumentDto>>
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly IMapper _mapper;

        public GetDocumentsByOfferIdHandler(IDocumentRepository documentRepository, IMapper mapper)
        {
            _documentRepository = documentRepository;
            _mapper = mapper;
        }

        public async Task<List<OfferDocumentDto>> Handle(GetDocumentsByOfferIdQuery request, CancellationToken cancellationToken)
        {
            var documents = await _documentRepository.GetDocumentsByOfferIdAsync(request.OfferId, request.UserId);
            return _mapper.Map<List<OfferDocumentDto>>(documents);
        }
    }

}
