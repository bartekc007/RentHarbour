using Xunit;
using Moq;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Net;
using System.Text.Json;
using RentHarbor.AuthService.Services;
using System.Net.Mime;
using Document.Application.Domain.Document.Common.Dto;
using Document.Application.Domain.Document.AddDocument;
using Document.Application.Domain.Document.GetDocumentsByOfferId;
using Document.Application.Domain.Document.GetDocumentById;
using Document.Api.Controllers;

namespace Document.Integration.Tests
{
    public class OfferDocumentsControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly Mock<IAuthorizationService> _authorizationServiceMock;
        private readonly OfferDocumentsController _controller;

        public OfferDocumentsControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _authorizationServiceMock = new Mock<IAuthorizationService>();
            _controller = new OfferDocumentsController(_mediatorMock.Object, _authorizationServiceMock.Object);
        }

        [Fact]
        public async Task UploadDocument_ShouldReturnOk_WithDocumentDetails()
        {
            var token = "valid-token";
            var userIdJson = "{\"userId\":\"user-id\"}";
            var request = new AddDocumentCommand { OfferId = 1, File = new FormFile(Stream.Null, 0, 0, "file", "file.pdf") };
            var commandResult = new OfferDocumentDto { DocumentId = "123", FileName = "file.pdf", UploadDate = DateTime.UtcNow };

            _authorizationServiceMock.Setup(a => a.GetUserIdFromTokenAsync(token)).ReturnsAsync(userIdJson);
            _mediatorMock.Setup(m => m.Send(It.IsAny<AddDocumentCommand>(), default)).ReturnsAsync(commandResult);

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };
            _controller.HttpContext.Request.Headers["Authorization"] = $"Bearer {token}";

            var response = await _controller.UploadDocument(request);

            var okResult = Assert.IsType<OkObjectResult>(response);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task GetDocumentsByOfferId_ShouldReturnOk_WithDocuments()
        {
            var token = "valid-token";
            var userIdJson = "{\"userId\":\"user-id\"}";
            var offerId = 1;
            var queryResult = new List<OfferDocumentDto> { new OfferDocumentDto { DocumentId = "123", FileName = "file.pdf" } };

            _authorizationServiceMock.Setup(a => a.GetUserIdFromTokenAsync(token)).ReturnsAsync(userIdJson);
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetDocumentsByOfferIdQuery>(), default)).ReturnsAsync(queryResult);

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };
            _controller.HttpContext.Request.Headers["Authorization"] = $"Bearer {token}";

            var response = await _controller.GetDocumentsByOfferId(offerId);

            var okResult = Assert.IsType<OkObjectResult>(response);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task DownloadDocument_ShouldReturnFile_WhenDocumentExists()
        {
            var token = "valid-token";
            var userIdJson = "{\"userId\":\"user-id\"}";
            var documentId = "123";
            var queryResult = new OfferDocumentDto { DocumentId = documentId, FileName = "file.pdf", Content = new byte[] { 1, 2, 3 } };

            _authorizationServiceMock.Setup(a => a.GetUserIdFromTokenAsync(token)).ReturnsAsync(userIdJson);
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetDocumentByIdQuery>(), default)).ReturnsAsync(queryResult);

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };
            _controller.HttpContext.Request.Headers["Authorization"] = $"Bearer {token}";

            var response = await _controller.DownloadDocument(documentId);

            var fileResult = Assert.IsType<FileContentResult>(response);
            Assert.Equal("application/pdf", fileResult.ContentType);
            Assert.Equal(queryResult.Content, fileResult.FileContents);
        }

        [Fact]
        public async Task DownloadDocument_ShouldReturnNotFound_WhenDocumentDoesNotExist()
        {
            var token = "valid-token";
            var userIdJson = "{\"userId\":\"user-id\"}";
            var documentId = "123";

            _authorizationServiceMock.Setup(a => a.GetUserIdFromTokenAsync(token)).ReturnsAsync(userIdJson);
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetDocumentByIdQuery>(), default)).ReturnsAsync((OfferDocumentDto)null);

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };
            _controller.HttpContext.Request.Headers["Authorization"] = $"Bearer {token}";

            var response = await _controller.DownloadDocument(documentId);

            var notFoundResult = Assert.IsType<NotFoundResult>(response);
            Assert.Equal(404, notFoundResult.StatusCode);
        }
    }
}

