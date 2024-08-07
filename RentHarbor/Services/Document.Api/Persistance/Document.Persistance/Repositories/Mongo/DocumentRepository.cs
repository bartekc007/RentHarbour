﻿using MongoDB.Driver;
using RentHarbor.MongoDb.Data;
using RentHarbor.MongoDb.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Document.Persistance.Repositories.Mongo
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly IMongoContext _context;

        public DocumentRepository(IMongoContext context)
        {
            _context = context;
        }

        public async Task AddDocumentAsync(OfferDocument document)
        {
            await _context.OfferDocuments.InsertOneAsync(document);
        }

        public async Task MarkDocumentsAsNotLatestAsync(int offerId)
        {
            var filter = Builders<OfferDocument>.Filter.Eq(doc => doc.OfferId, offerId);
            var update = Builders<OfferDocument>.Update.Set(doc => doc.IsLatest, false);
            await _context.OfferDocuments.UpdateManyAsync(filter, update);
        }

        public async Task<OfferDocument> GetLatestDocumentAsync(int offerId)
        {
            var filter = Builders<OfferDocument>.Filter.Eq(d => d.OfferId, offerId) &
                         Builders<OfferDocument>.Filter.Eq(d => d.IsLatest, true);
            return await _context.OfferDocuments.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<List<OfferDocument>> GetDocumentsByOfferIdAsync(int offerId, string userId)
        {
            var filter = Builders<OfferDocument>.Filter.Eq(d => d.OfferId, offerId) &
                         (Builders<OfferDocument>.Filter.Eq(d => d.OwnerId, userId) |
                          Builders<OfferDocument>.Filter.Eq(d => d.RenterId, userId));
            return await _context.OfferDocuments.Find(filter).SortByDescending(d => d.UploadDate).ToListAsync();
        }

        public async Task<OfferDocument> GetDocumentByIdAsync(string documentId)
        {
            var filter = Builders<OfferDocument>.Filter.Eq(d => d.DocumentId, documentId);
            return await _context.OfferDocuments.Find(filter).FirstOrDefaultAsync();
        }
    }

}
