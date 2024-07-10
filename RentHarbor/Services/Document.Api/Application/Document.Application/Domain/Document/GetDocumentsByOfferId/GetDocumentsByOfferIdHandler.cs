using AutoMapper;
using Document.Application.Domain.Document.Common.Dto;
using Document.Persistance.Repositories.Mongo;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Document.Application.Domain.Document.GetDocumentsByOfferId
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
