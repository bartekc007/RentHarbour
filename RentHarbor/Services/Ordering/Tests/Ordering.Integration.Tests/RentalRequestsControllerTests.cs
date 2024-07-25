using Moq;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Net;
using Ordering.Api.Controllers;
using Ordering.Api.Request;
using Ordering.Application.Domain.Order.AcceptRentalRequest;
using Ordering.Application.Domain.Order.CreateRentalRequest;
using Ordering.Application.Domain.Order.GetRentalOfferById;
using Ordering.Application.Domain.Order.GetRentalRequestByUserId;
using Ordering.Application.Domain.Order.GetRentalRequests;
using Ordering.Application.Domain.Order.Common;

namespace Ordering.Integration.Tests
{
    public class RentalRequestsControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly Mock<RentHarbor.AuthService.Services.IAuthorizationService> _authorizationServiceMock;
        private readonly RentalRequestsController _controller;

        public RentalRequestsControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _authorizationServiceMock = new Mock<RentHarbor.AuthService.Services.IAuthorizationService>();
            _controller = new RentalRequestsController(_mediatorMock.Object, _authorizationServiceMock.Object);
        }

        [Fact]
        public async Task CreateRentalRequest_ShouldReturnOk_WithResult()
        {
            var token = "valid-token";
            var userIdJson = "{\"userId\":\"user-id\"}";
            var request = new RentalRequestRequest
            {
                PropertyId = "property1",
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddDays(7),
                Pets = false,
                NumberOfPeople = 2,
                MessageToOwner = "Looking forward to renting!"
            };
            var commandResult = new CreateRentalRequestCommandResult { Status = "Success" };

            _authorizationServiceMock.Setup(a => a.GetUserIdFromTokenAsync(token)).ReturnsAsync(userIdJson);
            _mediatorMock.Setup(m => m.Send(It.IsAny<CreateRentalRequestCommand>(), default)).ReturnsAsync(commandResult);

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };
            _controller.HttpContext.Request.Headers["Authorization"] = $"Bearer {token}";

            var response = await _controller.CreateRentalRequest(request);

            var okResult = Assert.IsType<OkObjectResult>(response);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task GetRentalRequests_ShouldReturnOk_WithRentalRequests()
        {
            var token = "valid-token";
            var userIdJson = "{\"userId\":\"user-id\"}";
            var queryResult = new GetRentalRequestsQueryResult
            {
                Data = new List<RentalOffer>
            {
                new RentalOffer { Id = 1, PropertyId = "property1", Status = "Pending" }
            }
            };

            _authorizationServiceMock.Setup(a => a.GetUserIdFromTokenAsync(token)).ReturnsAsync(userIdJson);
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetRentalRequestsQuery>(), default)).ReturnsAsync(queryResult);

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };
            _controller.HttpContext.Request.Headers["Authorization"] = $"Bearer {token}";

            var response = await _controller.GetRentalRequests();

            var okResult = Assert.IsType<OkObjectResult>(response);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task GetRentalRequestsByUserId_ShouldReturnOk_WithRentalRequests()
        {
            var token = "valid-token";
            var userIdJson = "{\"userId\":\"user-id\"}";
            var queryResult = new GetRentalRequestByUserIdQueryResult
            {
                Data = new List<RentalOffer>
            {
                new RentalOffer { Id = 1, PropertyId = "property1", Status = "Approved" }
            }
            };

            _authorizationServiceMock.Setup(a => a.GetUserIdFromTokenAsync(token)).ReturnsAsync(userIdJson);
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetRentalRequestByUserIdQuery>(), default)).ReturnsAsync(queryResult);

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };
            _controller.HttpContext.Request.Headers["Authorization"] = $"Bearer {token}";

            var response = await _controller.GetRentalRequestsByUserId();

            var okResult = Assert.IsType<OkObjectResult>(response);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task GetRentalRequest_ShouldReturnOk_WithRentalRequest()
        {
            var token = "valid-token";
            var userIdJson = "{\"userId\":\"user-id\"}";
            var offerId = 1;
            var queryResult = new GetRentalRequestQueryResult
            {
                Data = new RentalOffer { Id = 1, PropertyId = "property1", Status = "Pending" }
            };

            _authorizationServiceMock.Setup(a => a.GetUserIdFromTokenAsync(token)).ReturnsAsync(userIdJson);
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetRentalRequestQuery>(), default)).ReturnsAsync(queryResult);

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };
            _controller.HttpContext.Request.Headers["Authorization"] = $"Bearer {token}";

            var response = await _controller.GetRentalRequest(offerId);

            var okResult = Assert.IsType<OkObjectResult>(response);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task GetRentalRequest_ShouldReturnNotFound_WhenRentalRequestDoesNotExist()
        {
            var token = "valid-token";
            var userIdJson = "{\"userId\":\"user-id\"}";
            var offerId = 1;

            _authorizationServiceMock.Setup(a => a.GetUserIdFromTokenAsync(token)).ReturnsAsync(userIdJson);
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetRentalRequestQuery>(), default)).ReturnsAsync(new GetRentalRequestQueryResult { Data = null });

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };
            _controller.HttpContext.Request.Headers["Authorization"] = $"Bearer {token}";

            var response = await _controller.GetRentalRequest(offerId);

            var okResult = Assert.IsType<OkObjectResult>(response);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task AcceptRentalRequest_ShouldReturnOk_WithSuccess()
        {
            var token = "valid-token";
            var userIdJson = "{\"userId\":\"user-id\"}";
            var command = new AcceptRentalRequestCommand { UserId = "user-id" };

            _authorizationServiceMock.Setup(a => a.GetUserIdFromTokenAsync(token)).ReturnsAsync(userIdJson);
            _mediatorMock.Setup(m => m.Send(It.IsAny<AcceptRentalRequestCommand>(), default)).ReturnsAsync(true);

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };
            _controller.HttpContext.Request.Headers["Authorization"] = $"Bearer {token}";

            var response = await _controller.AcceptRentalRequest(command);

            var okResult = Assert.IsType<OkObjectResult>(response);
            Assert.Equal(200, okResult.StatusCode);
        }
    }
}

