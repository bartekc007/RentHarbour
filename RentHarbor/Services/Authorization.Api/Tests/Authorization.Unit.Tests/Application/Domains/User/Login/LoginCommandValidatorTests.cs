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
            var command = new LoginCommand
            {
                UserName = "validUserName",
                Password = "validPassword123"
            };

            var result = _validator.TestValidate(command);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void Validate_ShouldHaveErrors_WhenUserNameIsEmpty()
        {
            var command = new LoginCommand
            {
                UserName = string.Empty,
                Password = "validPassword123"
            };

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(c => c.UserName).WithErrorMessage("Username is required.");
        }

        [Fact]
        public void Validate_ShouldHaveErrors_WhenPasswordIsEmpty()
        {
            var command = new LoginCommand
            {
                UserName = "validUserName",
                Password = string.Empty
            };

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(c => c.Password).WithErrorMessage("Password is required.");
        }

        [Fact]
        public void Validate_ShouldHaveErrors_WhenUserNameIsTooShort()
        {
            var command = new LoginCommand
            {
                UserName = "usr",
                Password = "validPassword123"
            };

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(c => c.UserName).WithErrorMessage("Username length must be between 5 and 50 characters.");
        }

        [Fact]
        public void Validate_ShouldHaveErrors_WhenPasswordIsTooShort()
        {
            var command = new LoginCommand
            {
                UserName = "validUserName",
                Password = "12345"
            };

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(c => c.Password).WithErrorMessage("Password length must be between 6 and 100 characters.");
        }
    }
}

