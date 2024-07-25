using Moq;
using Xunit;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Document.Application.Domain.Document.AddDocument;
using Document.Persistance.Repositories.Mongo;
using Document.Persistance.Repositories.Psql;
using RentHarbor.MongoDb.Entities;
using Microsoft.AspNetCore.Http;
using System.IO;
using System;
using System.Collections.Generic;
using Document.Persistance.Entities;
using Document.Application.Domain.Document.Common.Dto;


namespace Document.Unit.Tests.Application.Domains.Document.AddDocument
{
    public class AddDocumentCommandHandlerTests
    {
        private readonly Mock<IDocumentRepository> _documentRepositoryMock;
        private readonly Mock<IRentalRequestRepository> _rentalRequestRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly AddDocumentCommandHandler _handler;

        public AddDocumentCommandHandlerTests()
        {
            _documentRepositoryMock = new Mock<IDocumentRepository>();
            _rentalRequestRepositoryMock = new Mock<IRentalRequestRepository>();
            _mapperMock = new Mock<IMapper>();
            _handler = new AddDocumentCommandHandler(_documentRepositoryMock.Object, _rentalRequestRepositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnOfferDocumentDto_WhenDocumentIsAddedSuccessfully()
        {
            var fileMock = new Mock<IFormFile>();
            var content = "Hello World from a Fake File";
            var fileName = "test.pdf";
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(content);
            writer.Flush();
            ms.Position = 0;

            fileMock.Setup(f => f.OpenReadStream()).Returns(ms);
            fileMock.Setup(f => f.FileName).Returns(fileName);
            fileMock.Setup(f => f.Length).Returns(ms.Length);

            var command = new AddDocumentCommand
            {
                OfferId = 1,
                UserId = "user123",
                File = fileMock.Object
            };

            var rentalRequest = new RentalRequest
            {
                TenantId = "user123",
                UserId = "owner123"
            };

            var document = new OfferDocument
            {
                DocumentId = "document123",
                OfferId = 1,
                FileName = "test.pdf",
                Content = ms.ToArray(),
                UploadDate = DateTime.UtcNow,
                IsLatest = true
            };

            _documentRepositoryMock.Setup(repo => repo.MarkDocumentsAsNotLatestAsync(command.OfferId)).Returns(Task.CompletedTask);
            _rentalRequestRepositoryMock.Setup(repo => repo.GetRentalRequestByOfferIdAsync(command.OfferId)).ReturnsAsync(rentalRequest);
            _documentRepositoryMock.Setup(repo => repo.AddDocumentAsync(It.IsAny<OfferDocument>())).Returns(Task.CompletedTask);
            _mapperMock.Setup(m => m.Map<OfferDocumentDto>(It.IsAny<OfferDocument>())).Returns(new OfferDocumentDto { DocumentId = "document123" });

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal("document123", result.DocumentId);
        }

        [Fact]
        public async Task Handle_ShouldReturnNull_WhenRequestIsNull()
        {
            AddDocumentCommand command = null;

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.Null(result);
        }

        [Fact]
        public async Task Handle_ShouldReturnNull_WhenOfferIdIsInvalid()
        {
            var command = new AddDocumentCommand
            {
                OfferId = 0,
                UserId = "user123",
                File = new FormFile(new MemoryStream(), 0, 0, "Data", "test.pdf")
            };

            var rentalRequest = new RentalRequest
            {
                TenantId = "user123",
                UserId = "owner123"
            };

            _rentalRequestRepositoryMock.Setup(repo => repo.GetRentalRequestByOfferIdAsync(command.OfferId)).ReturnsAsync(rentalRequest);

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.Null(result);
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenRepositoryThrowsException()
        {
            var fileMock = new Mock<IFormFile>();
            var content = "Hello World from a Fake File";
            var fileName = "test.pdf";
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(content);
            writer.Flush();
            ms.Position = 0;

            fileMock.Setup(f => f.OpenReadStream()).Returns(ms);
            fileMock.Setup(f => f.FileName).Returns(fileName);
            fileMock.Setup(f => f.Length).Returns(ms.Length);

            var command = new AddDocumentCommand
            {
                OfferId = 1,
                UserId = "user123",
                File = fileMock.Object
            };

            _documentRepositoryMock.Setup(repo => repo.MarkDocumentsAsNotLatestAsync(command.OfferId)).ThrowsAsync(new Exception("Database error"));

            await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));
        }
    }
}

