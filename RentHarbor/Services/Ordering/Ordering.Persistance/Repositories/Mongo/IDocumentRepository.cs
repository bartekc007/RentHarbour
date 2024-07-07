using RentHarbor.MongoDb.Entities;

namespace Ordering.Persistance.Repositories.Mongo
{
    public interface IDocumentRepository
    {
        Task AddDocumentAsync(OfferDocument document);
        Task<OfferDocument> GetLatestDocumentAsync(int offerId);
        Task<List<OfferDocument>> GetDocumentsByOfferIdAsync(int offerId, string userId);
        Task MarkDocumentsAsNotLatestAsync(int offerId);
    }
}
