using FluentValidation.TestHelper;
using Catalog.Application.Domains.Property.GetPropertyById;

namespace Catalog.Unit.Tests.Application.Domains.GetPropertyById
{
    public class GetPropertyByIdQueryValidatorTests
    {
        private readonly GetPropertyByIdQueryValidator _validator;

        public GetPropertyByIdQueryValidatorTests()
        {
            _validator = new GetPropertyByIdQueryValidator();
        }

        [Fact]
        public void Validate_ShouldHaveNoErrors_WhenQueryIsValid()
        {
            // Arrange
            var query = new GetPropertyByIdQuery("valid-property-id");

            // Act
            var result = _validator.TestValidate(query);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void Validate_ShouldHaveErrors_WhenPropertyIdIsEmpty()
        {
            // Arrange
            var query = new GetPropertyByIdQuery("");

            // Act
            var result = _validator.TestValidate(query);

            // Assert
            result.ShouldHaveValidationErrorFor(q => q.PropertyId).WithErrorMessage("PropertyId cannot be empty.");
        }

        [Fact]
        public void Validate_ShouldHaveErrors_WhenPropertyIdContainsInvalidCharacters()
        {
            // Arrange
            var query = new GetPropertyByIdQuery("invalid-property-id!");

            // Act
            var result = _validator.TestValidate(query);

            // Assert
            result.ShouldHaveValidationErrorFor(q => q.PropertyId).WithErrorMessage("PropertyId contains invalid characters.");
        }
    }
}

