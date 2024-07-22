using FluentValidation.TestHelper;
using Authorization.Application.Domains.User.Register;
using Xunit;
using System;

namespace Authorization.Unit.Tests.Application.Domains.User.Register
{
    public class RegisterCommandValidatorTests
    {
        private readonly RegisterCommandValidator _validator;

        public RegisterCommandValidatorTests()
        {
            _validator = new RegisterCommandValidator();
        }

        [Fact]
        public void Validate_ShouldHaveNoErrors_WhenCommandIsValid()
        {
            // Arrange
            var command = new RegisterCommand
            {
                UserName = "validUserName",
                Email = "validemail@example.com",
                Password = "validPassword123",
                PhoneNumber = "1234567890",
                DateOfBirth = DateTime.Now.AddYears(-20),
                Address = "Valid Address",
                City = "Valid City",
                Country = "Valid Country"
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
            var command = new RegisterCommand
            {
                UserName = string.Empty,
                Email = "validemail@example.com",
                Password = "validPassword123",
                PhoneNumber = "1234567890",
                DateOfBirth = DateTime.Now.AddYears(-20),
                Address = "Valid Address",
                City = "Valid City",
                Country = "Valid Country"
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(c => c.UserName).WithErrorMessage("Username is required.");
        }

        [Fact]
        public void Validate_ShouldHaveErrors_WhenEmailIsInvalid()
        {
            // Arrange
            var command = new RegisterCommand
            {
                UserName = "validUserName",
                Email = "invalid-email",
                Password = "validPassword123",
                PhoneNumber = "1234567890",
                DateOfBirth = DateTime.Now.AddYears(-20),
                Address = "Valid Address",
                City = "Valid City",
                Country = "Valid Country"
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(c => c.Email).WithErrorMessage("Invalid email format.");
        }

        [Fact]
        public void Validate_ShouldHaveErrors_WhenPasswordIsTooShort()
        {
            // Arrange
            var command = new RegisterCommand
            {
                UserName = "validUserName",
                Email = "validemail@example.com",
                Password = "short",
                PhoneNumber = "1234567890",
                DateOfBirth = DateTime.Now.AddYears(-20),
                Address = "Valid Address",
                City = "Valid City",
                Country = "Valid Country"
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(c => c.Password).WithErrorMessage("Password length must be between 6 and 100 characters.");
        }

        // Dodaj więcej testów walidatora dla innych przypadków
    }
}
