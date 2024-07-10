using Document.Application.Domain.Document.Common.Dto;
using MediatR;
using System.Collections.Generic;

namespace Document.Application.Domain.Document.GetDocumentsByOfferId
{
    public class GetDocumentsByOfferIdQuery : IRequest<List<OfferDocumentDto>>
    {
        public int OfferId { get; set; }
        public string UserId { get; set; }
    }
}
