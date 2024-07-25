using Xunit;
using Moq;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using RentHarbor.AuthService.Services;
using Basket.Api.Controllers;
using Basket.Api.Requests;
using Basket.Application.Domains.Basket.UpdateBasket;
using Microsoft.AspNetCore.Http;

namespace Basket.Integration.Tests
{
    public class BasketControllerTests
    {
        private readonly Mock<IAuthorizationService> _authorizationServiceMock;
        private readonly Mock<IMediator> _mediatorMock;
        private readonly BasketController _controller;

        public BasketControllerTests()
        {
            _authorizationServiceMock = new Mock<IAuthorizationService>();
            _mediatorMock = new Mock<IMediator>();
            _controller = new BasketController(_authorizationServiceMock.Object, _mediatorMock.Object);
        }

        [Fact]
        public async Task UpdateBasket_ShouldReturnOk_WhenUserIsAuthorized()
        {
            var token = "valid-token";
            var userId = "user1";
            var request = new UpdateBasketRequest { PropertyId = "property1" };

            _authorizationServiceMock.Setup(auth => auth.GetUserIdFromTokenAsync(token))
                .ReturnsAsync(userId);

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };
            _controller.HttpContext.Request.Headers["Authorization"] = $"Bearer {token}";

            var commandResult = new UpdateBasketResult { Success = true };
            _mediatorMock.Setup(m => m.Send(It.IsAny<UpdateBasketCommand>(), default))
                .ReturnsAsync(commandResult);

            var result = await _controller.UpdateBasket(request);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task UpdateBasket_ShouldReturnUnauthorized_WhenUserIsNotAuthorized()
        {
            var token = "invalid-token";
            var request = new UpdateBasketRequest { PropertyId = "property1" };

            _authorizationServiceMock.Setup(auth => auth.GetUserIdFromTokenAsync(token))
                .ReturnsAsync((string)null);

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };
            _controller.HttpContext.Request.Headers["Authorization"] = $"Bearer {token}";

            var result = await _controller.UpdateBasket(request);

            var unauthorizedResult = Assert.IsType<UnauthorizedResult>(result);
            Assert.Equal(401, unauthorizedResult.StatusCode);
        }
    }
}

