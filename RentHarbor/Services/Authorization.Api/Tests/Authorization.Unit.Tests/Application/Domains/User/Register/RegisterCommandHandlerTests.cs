using Moq;
using Authorization.Infrastructure.Services.Jwt;
using Authorization.Persistance.Entities;
using Authorization.Application.Domains.User.Register;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Authorization.Unit.Tests.Application.Domains.User.Register
{
    public class RegisterCommandHandlerTests
    {
        private readonly Mock<UserManager<ApplicationUser>> _userManagerMock;
        private readonly Mock<IJwtService> _jwtServiceMock;
        private readonly RegisterCommandHandler _handler;
        private readonly PasswordHasher<ApplicationUser> _passwordHasher;

        public RegisterCommandHandlerTests()
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

            _jwtServiceMock = new Mock<IJwtService>();
            _handler = new RegisterCommandHandler(_userManagerMock.Object, _jwtServiceMock.Object);

            _passwordHasher = new PasswordHasher<ApplicationUser>();
        }

        [Fact]
        public async Task Handle_ShouldReturnRegisterResult_WhenRegistrationIsSuccessful()
        {
            var command = new RegisterCommand
            {
                UserName = "validUser",
                Email = "validuser@example.com",
                Password = "validPassword123",
                PhoneNumber = "1234567890",
                DateOfBirth = DateTime.Now.AddYears(-20),
                Address = "Valid Address",
                City = "Valid City",
                Country = "Valid Country"
            };

            var user = new ApplicationUser
            {
                UserName = command.UserName,
                Email = command.Email,
                PhoneNumber = command.PhoneNumber,
                DateOfBirth = command.DateOfBirth,
                Address = command.Address,
                City = command.City,
                Country = command.Country
            };

            _userManagerMock.Setup(um => um.CreateAsync(It.IsAny<ApplicationUser>(), command.Password))
                .ReturnsAsync(IdentityResult.Success);

            _jwtServiceMock.Setup(js => js.GenerateAccessToken(It.IsAny<ApplicationUser>()))
                .Returns("accessToken");
            _jwtServiceMock.Setup(js => js.GenerateRefreshToken())
                .Returns("refreshToken");

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.Equal(command.UserName, result.Username);
            Assert.Equal("accessToken", result.AccessToken);
            Assert.Equal("refreshToken", result.ReffreshToken);
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenRegistrationFails()
        {
            var command = new RegisterCommand
            {
                UserName = "validUser",
                Email = "validuser@example.com",
                Password = "validPassword123",
                PhoneNumber = "1234567890",
                DateOfBirth = DateTime.Now.AddYears(-20),
                Address = "Valid Address",
                City = "Valid City",
                Country = "Valid Country"
            };

            _userManagerMock.Setup(um => um.CreateAsync(It.IsAny<ApplicationUser>(), command.Password))
                .ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "Registration failed." }));

            await Assert.ThrowsAsync<ApplicationException>(() => _handler.Handle(command, CancellationToken.None));
        }
    }
}


