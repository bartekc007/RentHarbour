using MediatR;
using Ordering.Application.Domain.Document.Common.Dto;
using RentHarbor.MongoDb.Entities;

namespace Ordering.Application.Domain.Document.GetDocumentsByOfferId
{
    public class GetDocumentsByOfferIdQuery : IRequest<List<OfferDocumentDto>>
    {
        public int OfferId { get; set; }
        public string UserId { get; set; }
    }
}
