using MongoDB.Driver;
using Moq;
using RentHarbor.MongoDb.Data;
using RentHarbor.MongoDb.Entities;
using Document.Persistance.Repositories.Mongo;

namespace Document.Unit.Tests.Persistance.Repositories
{
    public class DocumentRepositoryTests
    {
        private readonly Mock<IMongoContext> _contextMock;
        private readonly Mock<IMongoCollection<OfferDocument>> _offerDocumentCollectionMock;
        private readonly DocumentRepository _repository;

        public DocumentRepositoryTests()
        {
            _contextMock = new Mock<IMongoContext>();
            _offerDocumentCollectionMock = new Mock<IMongoCollection<OfferDocument>>();
            _contextMock.SetupGet(ctx => ctx.OfferDocuments).Returns(_offerDocumentCollectionMock.Object);
            _repository = new DocumentRepository(_contextMock.Object);
        }

        [Fact]
        public async Task AddDocumentAsync_ShouldInsertDocument()
        {
            var document = new OfferDocument { DocumentId = "document1", OfferId = 1, FileName = "test.pdf" };

            await _repository.AddDocumentAsync(document);

            _offerDocumentCollectionMock.Verify(coll => coll.InsertOneAsync(document, null, default), Times.Once);
        }
    }
}

