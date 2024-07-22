using Moq;
using Document.Application.Domain.Document.GetDocumentById;
using Document.Persistance.Repositories.Mongo;
using RentHarbor.MongoDb.Entities;

namespace Document.Unit.Tests.Application.Domains.Document.GetDocumentById
{
    public class GetDocumentByIdQueryHandlerTests
    {
        private readonly Mock<IDocumentRepository> _documentRepositoryMock;
        private readonly GetDocumentByIdQueryHandler _handler;

        public GetDocumentByIdQueryHandlerTests()
        {
            _documentRepositoryMock = new Mock<IDocumentRepository>();
            _handler = new GetDocumentByIdQueryHandler(_documentRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnDocumentDto_WhenDocumentExists()
        {
            // Arrange
            var documentId = "5f47ac7b8452ef001d4fb9c5";
            var query = new GetDocumentByIdQuery { DocumentId = documentId };

            var document = new OfferDocument
            {
                DocumentId = documentId,
                OfferId = 1,
                FileName = "test.pdf",
                Content = new byte[] { 0x20, 0x20, 0x20 },
                UploadDate = DateTime.UtcNow,
                IsLatest = true
            };

            _documentRepositoryMock.Setup(repo => repo.GetDocumentByIdAsync(documentId)).ReturnsAsync(document);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(documentId, result.DocumentId);
        }

        [Fact]
        public async Task Handle_ShouldReturnNull_WhenDocumentDoesNotExist()
        {
            // Arrange
            var documentId = "5f47ac7b8452ef001d4fb9c5";
            var query = new GetDocumentByIdQuery { DocumentId = documentId };

            _documentRepositoryMock.Setup(repo => repo.GetDocumentByIdAsync(documentId)).ReturnsAsync((OfferDocument)null);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Null(result);
        }
    }
}

