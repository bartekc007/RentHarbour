using FluentValidation.TestHelper;
using Catalog.Application.Domains.Property.GetRentedProperites;

namespace Catalog.Unit.Tests.Application.Domains.GetRentedProperties
{
    public class GetRentedPropertiesQueryValidatorTests
    {
        private readonly GetRentedPropertiesQueryValidator _validator;

        public GetRentedPropertiesQueryValidatorTests()
        {
            _validator = new GetRentedPropertiesQueryValidator();
        }

        [Fact]
        public void Validate_ShouldHaveNoErrors_WhenQueryIsValid()
        {
            var query = new GetRentedPropertiesQuery { UserId = "valid-user-id" };

            var result = _validator.TestValidate(query);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void Validate_ShouldHaveErrors_WhenUserIdIsEmpty()
        {
            var query = new GetRentedPropertiesQuery { UserId = "" };

            var result = _validator.TestValidate(query);

            result.ShouldHaveValidationErrorFor(q => q.UserId).WithErrorMessage("UserId cannot be empty.");
        }

        [Fact]
        public void Validate_ShouldHaveErrors_WhenUserIdContainsInvalidCharacters()
        {
            var query = new GetRentedPropertiesQuery { UserId = "invalid-user-id!" };

            var result = _validator.TestValidate(query);

            result.ShouldHaveValidationErrorFor(q => q.UserId).WithErrorMessage("UserId contains invalid characters.");
        }
    }
}

