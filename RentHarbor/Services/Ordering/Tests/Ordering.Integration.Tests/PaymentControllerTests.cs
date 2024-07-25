using Moq;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Net;
using RentHarbor.AuthService.Services;
using Ordering.Api.Controllers;
using Ordering.Application.Domain.Payment.GetPayments.Dto;
using Ordering.Application.Domain.Payment.GetPayments;

namespace Ordering.Integration.Tests
{
    public class PaymentControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly Mock<IAuthorizationService> _authorizationServiceMock;
        private readonly PaymentController _controller;

        public PaymentControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _authorizationServiceMock = new Mock<IAuthorizationService>();
            _controller = new PaymentController(_mediatorMock.Object, _authorizationServiceMock.Object);
        }

        [Fact]
        public async Task GetPayments_ShouldReturnOk_WithPayments()
        {
            var token = "valid-token";
            var userIdJson = "{\"userId\":\"user-id\"}";
            var propertyId = "property1";
            var queryResult = new List<PaymentDto> { new PaymentDto { Id = "1", Amount = 100.00m } };

            _authorizationServiceMock.Setup(a => a.GetUserIdFromTokenAsync(token)).ReturnsAsync(userIdJson);
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetPaymentsQuery>(), default)).ReturnsAsync(queryResult);

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };
            _controller.HttpContext.Request.Headers["Authorization"] = $"Bearer {token}";

            var response = await _controller.GetPayments(propertyId);

            var okResult = Assert.IsType<OkObjectResult>(response);
            Assert.Equal(200, okResult.StatusCode);
        }
    }
}

