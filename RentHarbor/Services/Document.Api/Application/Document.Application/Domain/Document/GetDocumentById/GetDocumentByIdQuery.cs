using Document.Application.Domain.Document.Common.Dto;
using MediatR;

namespace Document.Application.Domain.Document.GetDocumentById
{
    public class GetDocumentByIdQuery : IRequest<OfferDocumentDto>
    {
        public string DocumentId { get; set; }
    }

}
