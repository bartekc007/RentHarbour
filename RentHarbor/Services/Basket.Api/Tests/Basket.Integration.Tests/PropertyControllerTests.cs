using Xunit;
using Moq;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using RentHarbor.AuthService.Services;
using Basket.Api.Controllers;
using Basket.Api.Requests;
using Microsoft.AspNetCore.Http;
using Basket.Application.Domains.Basket.GetFollowedProperties;
using Basket.Application.Domains.Basket.GetFollowedProperties.Dto;

namespace Basket.Integration.Tests
{
    public class PropertyControllerTests
    {
        private readonly Mock<IAuthorizationService> _authorizationServiceMock;
        private readonly Mock<IMediator> _mediatorMock;
        private readonly PropertyController _controller;

        public PropertyControllerTests()
        {
            _authorizationServiceMock = new Mock<IAuthorizationService>();
            _mediatorMock = new Mock<IMediator>();
            _controller = new PropertyController(_authorizationServiceMock.Object, _mediatorMock.Object);
        }

        [Fact]
        public async Task GetAllFollowedProperties_ShouldReturnOk_WhenUserIsAuthorized()
        {
            var token = "valid-token";
            var userId = "user1";
            var request = new UpdateBasketRequest();

            _authorizationServiceMock.Setup(auth => auth.GetUserIdFromTokenAsync(token))
                .ReturnsAsync(userId);

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };
            _controller.HttpContext.Request.Headers["Authorization"] = $"Bearer {token}";

            var commandResult = new GetFollowedPropertiesResult { Data = new List<PropertyDto>() };
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetFollowedPropertiesQuery>(), default))
                .ReturnsAsync(commandResult);

            var result = await _controller.GetAllFollowedProperties(request);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task GetAllFollowedProperties_ShouldReturnUnauthorized_WhenUserIsNotAuthorized()
        {
            var token = "invalid-token";
            var request = new UpdateBasketRequest();

            _authorizationServiceMock.Setup(auth => auth.GetUserIdFromTokenAsync(token))
                .ReturnsAsync((string)null);

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };
            _controller.HttpContext.Request.Headers["Authorization"] = $"Bearer {token}";

            var result = await _controller.GetAllFollowedProperties(request);

            var unauthorizedResult = Assert.IsType<UnauthorizedResult>(result);
            Assert.Equal(401, unauthorizedResult.StatusCode);
        }
    }
}

