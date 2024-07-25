using Moq;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Authorization.Api.Controllers.User;
using Authorization.Application.Domains.User.Login;
using Authorization.Application.Domains.User.RefreshToken;
using Authorization.Application.Domains.User.Register;
using Authorization.Infrastructure.Services.Jwt;
using Authorization.Persistance.Context;

namespace Authorization.Integration.Tests
{
    public class UserControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly Mock<IJwtService> _jwtServiceMock;
        private readonly Mock<AuthDbContext> _authDbContextMock;
        private readonly UserController _controller;

        public UserControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _jwtServiceMock = new Mock<IJwtService>();
            _authDbContextMock = new Mock<AuthDbContext>();
            _controller = new UserController(_mediatorMock.Object, _jwtServiceMock.Object, _authDbContextMock.Object);
        }

        [Fact]
        public async Task Register_ShouldReturnOk_WhenUserIsRegisteredSuccessfully()
        {
            var command = new RegisterCommand();
            var result = new RegisterResult();
            _mediatorMock.Setup(m => m.Send(command, default)).ReturnsAsync(result);

            var response = await _controller.Register(command);

            var okResult = Assert.IsType<OkObjectResult>(response);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task Login_ShouldReturnOk_WithAccessToken()
        {
            var command = new LoginCommand();
            var result = new LoginResult { AccessToken = "valid-token" };
            _mediatorMock.Setup(m => m.Send(command, default)).ReturnsAsync(result);

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };

            var response = await _controller.Login(command);

            var okResult = Assert.IsType<OkObjectResult>(response);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal("Bearer valid-token", _controller.HttpContext.Response.Headers["Authorization"]);
        }

        [Fact]
        public async Task RefreshToken_ShouldReturnOk_WithNewAccessToken()
        {
            var refreshToken = "valid-refresh-token";
            var result = RefreshTokenResult.Ok("new-access-token", "new-refresh-token");
            _mediatorMock.Setup(m => m.Send(It.IsAny<RefreshTokenCommand>(), default)).ReturnsAsync(result);

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };
            _controller.HttpContext.Request.Headers["Authorization"] = $"Bearer {refreshToken}";

            var response = await _controller.RefreshToken();

            var okResult = Assert.IsType<OkObjectResult>(response);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal("Bearer new-access-token", _controller.HttpContext.Response.Headers["Authorization"]);
        }

        [Fact]
        public async Task GetUserIdFromTokenAsync_ShouldReturnOk_WhenTokenIsValid()
        {
            var token = "valid-token";
            var userId = "user-id";
            _jwtServiceMock.Setup(j => j.GetPrincipalFromToken(token)).Returns(userId);

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };
            _controller.HttpContext.Request.Headers["Authorization"] = $"Bearer {token}";

            var response = await _controller.GetUserIdFromTokenAsync();

            var okResult = Assert.IsType<OkObjectResult>(response);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task GetUserIdFromTokenAsync_ShouldReturnUnauthorized_WhenTokenIsInvalid()
        {
            var token = "invalid-token";
            _jwtServiceMock.Setup(j => j.GetPrincipalFromToken(token)).Returns((string)null);

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };
            _controller.HttpContext.Request.Headers["Authorization"] = $"Bearer {token}";

            var response = await _controller.GetUserIdFromTokenAsync();

            var unauthorizedResult = Assert.IsType<UnauthorizedResult>(response);
            Assert.Equal(401, unauthorizedResult.StatusCode);
        }

        [Fact]
        public async Task GetUserNameById_ShouldReturnUnauthorized_WhenTokenIsInvalid()
        {
            var token = "invalid-token";
            _jwtServiceMock.Setup(j => j.GetPrincipalFromToken(token)).Returns((string)null);

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };
            _controller.HttpContext.Request.Headers["Authorization"] = $"Bearer {token}";

            var response = await _controller.GetUserNameById();

            var unauthorizedResult = Assert.IsType<UnauthorizedResult>(response);
            Assert.Equal(401, unauthorizedResult.StatusCode);
        }
    }
}

