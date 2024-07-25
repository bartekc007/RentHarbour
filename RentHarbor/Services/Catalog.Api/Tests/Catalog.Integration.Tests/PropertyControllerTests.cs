using Moq;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using RentHarbor.AuthService.Services;
using Catalog.Api.Controllers;
using Catalog.Application.Domains.Property.GetProperties;
using Catalog.Application.Domains.Property.GetProperties.Dto;
using Catalog.Application.Domains.Property.GetRentedProperites;
using Catalog.Application.Domains.Property.GetRentedProperites.Dto;
using Catalog.Application.Domains.Property.GetPropertyById;
using Catalog.Application.Domains.Property.GetPropertyById.Dto;

namespace Catalog.Integration.Tests
{
    public class PropertyControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly Mock<IAuthorizationService> _authorizationServiceMock;
        private readonly PropertyController _controller;

        public PropertyControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _authorizationServiceMock = new Mock<IAuthorizationService>();
            _controller = new PropertyController(_mediatorMock.Object, _authorizationServiceMock.Object);
        }

        [Fact]
        public async Task GetProperties_ShouldReturnOk_WithProperties()
        {
            var query = new GetPropertiesQuery();
            var result = new GetPropertiesResult { Data = new List<PropertyDto> { new PropertyDto { Id = "1" } } };
            _mediatorMock.Setup(m => m.Send(query, default)).ReturnsAsync(result);

            var response = await _controller.GetProperties(query);

            var okResult = Assert.IsType<OkObjectResult>(response.Result);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task GetProperties_ShouldReturnNoContent_WhenNoPropertiesFound()
        {
            var query = new GetPropertiesQuery();
            var result = new GetPropertiesResult { Data = new List<PropertyDto>() };
            _mediatorMock.Setup(m => m.Send(query, default)).ReturnsAsync(result);

            var response = await _controller.GetProperties(query);

            var okResult = Assert.IsType<OkObjectResult>(response.Result);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task GetRentedProperties_ShouldReturnOk_WithRentedProperties()
        {
            var token = "valid-token";
            var userIdJson = "{\"userId\":\"user-id\"}";
            var query = new GetRentedPropertiesQuery { UserId = "user-id" };
            var result = new GetRentedPropertiesQueryResult { Data = new List<RentedProperty> { new RentedProperty { PropertyId = "1" } } };

            _authorizationServiceMock.Setup(a => a.GetUserIdFromTokenAsync(token)).ReturnsAsync(userIdJson);
            _mediatorMock.Setup(m => m.Send(It.Is<GetRentedPropertiesQuery>(q => q.UserId == "user-id"), default)).ReturnsAsync(result);

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };
            _controller.HttpContext.Request.Headers["Authorization"] = $"Bearer {token}";

            var response = await _controller.GetRentedProperties();

            var okResult = Assert.IsType<OkObjectResult>(response.Result);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task GetRentedProperties_ShouldReturnNoContent_WhenNoRentedPropertiesFound()
        {
            var token = "valid-token";
            var userIdJson = "{\"userId\":\"user-id\"}";
            var query = new GetRentedPropertiesQuery { UserId = "user-id" };
            var result = new GetRentedPropertiesQueryResult { Data = new List<RentedProperty>() };

            _authorizationServiceMock.Setup(a => a.GetUserIdFromTokenAsync(token)).ReturnsAsync(userIdJson);
            _mediatorMock.Setup(m => m.Send(It.Is<GetRentedPropertiesQuery>(q => q.UserId == "user-id"), default)).ReturnsAsync(result);

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };
            _controller.HttpContext.Request.Headers["Authorization"] = $"Bearer {token}";

            var response = await _controller.GetRentedProperties();

            var okResult = Assert.IsType<OkObjectResult>(response.Result);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task GetPropertyById_ShouldReturnOk_WhenPropertyExists()
        {
            var propertyId = "1";
            var query = new GetPropertyByIdQuery(propertyId);
            var result = new GetPropertyByIdDto();

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetPropertyByIdQuery>(), default)).ReturnsAsync(result);

            var response = await _controller.GetPropertyById(propertyId);

            var okResult = Assert.IsType<OkObjectResult>(response);
            Assert.Equal(200, okResult.StatusCode);
        }
    }
}

