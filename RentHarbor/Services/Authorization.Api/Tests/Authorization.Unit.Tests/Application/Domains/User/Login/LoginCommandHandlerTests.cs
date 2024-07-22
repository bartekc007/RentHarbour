using Moq;
using Authorization.Infrastructure.Services.Jwt;
using Authorization.Persistance.Context;
using Authorization.Persistance.Entities;
using Authorization.Application.Domains.User.Login;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;

namespace Authorization.Unit.Tests.Application.Domains.User.Login
{
    public class LoginCommandHandlerTests
    {
        private readonly Mock<UserManager<ApplicationUser>> _userManagerMock;
        private readonly Mock<SignInManager<ApplicationUser>> _signInManagerMock;
        private readonly Mock<IJwtService> _jwtServiceMock;
        private readonly DbContextOptions<AuthDbContext> _dbContextOptions;
        private readonly LoginCommandHandler _handler;
        private readonly PasswordHasher<ApplicationUser> _passwordHasher;

        public LoginCommandHandlerTests()
        {
            _userManagerMock = new Mock<UserManager<ApplicationUser>>(
                new Mock<IUserStore<ApplicationUser>>().Object,
                new Mock<IOptions<IdentityOptions>>().Object,
                new Mock<IPasswordHasher<ApplicationUser>>().Object,
                new IUserValidator<ApplicationUser>[0],
                new IPasswordValidator<ApplicationUser>[0],
                new Mock<ILookupNormalizer>().Object,
                new Mock<IdentityErrorDescriber>().Object,
                new Mock<IServiceProvider>().Object,
                new Mock<ILogger<UserManager<ApplicationUser>>>().Object);

            _signInManagerMock = new Mock<SignInManager<ApplicationUser>>(
                _userManagerMock.Object,
                new Mock<IHttpContextAccessor>().Object,
                new Mock<IUserClaimsPrincipalFactory<ApplicationUser>>().Object,
                new Mock<IOptions<IdentityOptions>>().Object,
                new Mock<ILogger<SignInManager<ApplicationUser>>>().Object,
                new Mock<IAuthenticationSchemeProvider>().Object,
                new Mock<IUserConfirmation<ApplicationUser>>().Object);

            _jwtServiceMock = new Mock<IJwtService>();

            _dbContextOptions = new DbContextOptionsBuilder<AuthDbContext>()
                .UseInMemoryDatabase(databaseName: "AuthDatabase")
                .Options;

            var authDbContext = new AuthDbContext(_dbContextOptions);
            _handler = new LoginCommandHandler(_userManagerMock.Object, _signInManagerMock.Object, _jwtServiceMock.Object, authDbContext);

            _passwordHasher = new PasswordHasher<ApplicationUser>();
        }

        [Fact]
        public async Task Handle_ShouldReturnLoginResult_WhenCredentialsAreValid()
        {
            // Arrange
            var command = new LoginCommand { UserName = "validUser", Password = "validPassword" };
            var user = new ApplicationUser
            {
                UserName = command.UserName,
                PasswordHash = _passwordHasher.HashPassword(null, command.Password),
                Address = "Valid Address",
                City = "Valid City",
                Country = "Valid Country",
                Email = "validuser@example.com"
            };

            using (var context = new AuthDbContext(_dbContextOptions))
            {
                context.Users.Add(user);
                context.SaveChanges();
            }

            _userManagerMock.Setup(um => um.CheckPasswordAsync(It.IsAny<ApplicationUser>(), command.Password))
                .ReturnsAsync(true);

            _jwtServiceMock.Setup(js => js.GenerateAccessToken(It.IsAny<ApplicationUser>()))
                .Returns("accessToken");
            _jwtServiceMock.Setup(js => js.GenerateRefreshToken())
                .Returns("refreshToken");

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(command.UserName, result.UserName);
            Assert.Equal("accessToken", result.AccessToken);
            Assert.Equal("refreshToken", result.RefreshToken);
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenUserDoesNotExist()
        {
            // Arrange
            var command = new LoginCommand { UserName = "invalidUser", Password = "password" };

            // Act & Assert
            await Assert.ThrowsAsync<ApplicationException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenPasswordIsInvalid()
        {
            // Arrange
            var command = new LoginCommand { UserName = "validUser", Password = "invalidPassword" };
            var user = new ApplicationUser
            {
                UserName = command.UserName,
                PasswordHash = _passwordHasher.HashPassword(null, "validPassword"),
                Address = "Valid Address",
                City = "Valid City",
                Country = "Valid Country",
                Email = "validuser@example.com"
            };

            using (var context = new AuthDbContext(_dbContextOptions))
            {
                context.Users.Add(user);
                context.SaveChanges();
            }

            _userManagerMock.Setup(um => um.CheckPasswordAsync(It.IsAny<ApplicationUser>(), command.Password))
                .ReturnsAsync(false);

            // Act & Assert
            await Assert.ThrowsAsync<ApplicationException>(() => _handler.Handle(command, CancellationToken.None));
        }
    }
}


