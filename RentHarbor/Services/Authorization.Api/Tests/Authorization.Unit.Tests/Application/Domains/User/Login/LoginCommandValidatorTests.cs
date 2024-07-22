using FluentValidation.TestHelper;
using Authorization.Application.Domains.User.Login;

namespace Authorization.Unit.Tests.Application.Domains.User.Login
{
    public class LoginCommandValidatorTests
    {
        private readonly LoginCommandValidator _validator;

        public LoginCommandValidatorTests()
        {
            _validator = new LoginCommandValidator();
        }

        [Fact]
        public void Validate_ShouldHaveNoErrors_WhenCommandIsValid()
        {
            // Arrange
            var command = new LoginCommand
            {
                UserName = "validUserName",
                Password = "validPassword123"
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void Validate_ShouldHaveErrors_WhenUserNameIsEmpty()
        {
            // Arrange
            var command = new LoginCommand
            {
                UserName = string.Empty,
                Password = "validPassword123"
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(c => c.UserName).WithErrorMessage("Username is required.");
        }

        [Fact]
        public void Validate_ShouldHaveErrors_WhenPasswordIsEmpty()
        {
            // Arrange
            var command = new LoginCommand
            {
                UserName = "validUserName",
                Password = string.Empty
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(c => c.Password).WithErrorMessage("Password is required.");
        }

        [Fact]
        public void Validate_ShouldHaveErrors_WhenUserNameIsTooShort()
        {
            // Arrange
            var command = new LoginCommand
            {
                UserName = "usr",
                Password = "validPassword123"
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(c => c.UserName).WithErrorMessage("Username length must be between 5 and 50 characters.");
        }

        [Fact]
        public void Validate_ShouldHaveErrors_WhenPasswordIsTooShort()
        {
            // Arrange
            var command = new LoginCommand
            {
                UserName = "validUserName",
                Password = "12345"
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(c => c.Password).WithErrorMessage("Password length must be between 6 and 100 characters.");
        }
    }
}

