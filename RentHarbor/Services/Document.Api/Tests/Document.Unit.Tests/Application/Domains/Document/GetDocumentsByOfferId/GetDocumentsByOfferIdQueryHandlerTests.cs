using Moq;
using AutoMapper;
using Document.Application.Domain.Document.GetDocumentsByOfferId;
using Document.Persistance.Repositories.Mongo;
using RentHarbor.MongoDb.Entities;
using Document.Application.Domain.Document.Common.Dto;

namespace Document.Unit.Tests.Application.Domains.Document.GetDocumentsByOfferId
{
    public class GetDocumentsByOfferIdQueryHandlerTests
    {
        private readonly Mock<IDocumentRepository> _documentRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly GetDocumentsByOfferIdQueryHandler _handler;

        public GetDocumentsByOfferIdQueryHandlerTests()
        {
            _documentRepositoryMock = new Mock<IDocumentRepository>();
            _mapperMock = new Mock<IMapper>();
            _handler = new GetDocumentsByOfferIdQueryHandler(_documentRepositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnDocuments_WhenDocumentsExist()
        {
            // Arrange
            var offerId = 1;
            var query = new GetDocumentsByOfferIdQuery { OfferId = offerId };

            var documents = new List<OfferDocument>
        {
            new OfferDocument
            {
                DocumentId = "document1",
                OfferId = offerId,
                FileName = "test1.pdf",
                Content = new byte[] { 0x20, 0x20, 0x20 },
                UploadDate = DateTime.UtcNow,
                IsLatest = true
            },
            new OfferDocument
            {
                DocumentId = "document2",
                OfferId = offerId,
                FileName = "test2.pdf",
                Content = new byte[] { 0x20, 0x20, 0x20 },
                UploadDate = DateTime.UtcNow,
                IsLatest = true
            }
        };

            var documentDtos = new List<OfferDocumentDto>
        {
            new OfferDocumentDto
            {
                DocumentId = "document1",
                FileName = "test1.pdf",
                Content = new byte[] { 0x20, 0x20, 0x20 }
            },
            new OfferDocumentDto
            {
                DocumentId = "document2",
                FileName = "test2.pdf",
                Content = new byte[] { 0x20, 0x20, 0x20 }
            }
        };

            _documentRepositoryMock.Setup(repo => repo.GetDocumentsByOfferIdAsync(It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(documents);
            _mapperMock.Setup(m => m.Map<IEnumerable<OfferDocumentDto>>(documents)).Returns(documentDtos);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task Handle_ShouldReturnEmptyList_WhenNoDocumentsExist()
        {
            // Arrange
            var offerId = 1;
            var query = new GetDocumentsByOfferIdQuery { OfferId = offerId };

            _documentRepositoryMock.Setup(repo => repo.GetDocumentsByOfferIdAsync(It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(new List<OfferDocument>());
            _mapperMock.Setup(m => m.Map<IEnumerable<OfferDocumentDto>>(It.IsAny<IEnumerable<OfferDocument>>())).Returns(new List<OfferDocumentDto>());

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }
    }
}

