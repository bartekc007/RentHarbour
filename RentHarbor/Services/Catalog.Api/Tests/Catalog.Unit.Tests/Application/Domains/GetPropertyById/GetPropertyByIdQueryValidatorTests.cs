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
            var query = new GetPropertyByIdQuery("valid-property-id");

            var result = _validator.TestValidate(query);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void Validate_ShouldHaveErrors_WhenPropertyIdIsEmpty()
        {
            var query = new GetPropertyByIdQuery("");

            var result = _validator.TestValidate(query);

            result.ShouldHaveValidationErrorFor(q => q.PropertyId).WithErrorMessage("PropertyId cannot be empty.");
        }

        [Fact]
        public void Validate_ShouldHaveErrors_WhenPropertyIdContainsInvalidCharacters()
        {
            var query = new GetPropertyByIdQuery("invalid-property-id!");

            var result = _validator.TestValidate(query);

            result.ShouldHaveValidationErrorFor(q => q.PropertyId).WithErrorMessage("PropertyId contains invalid characters.");
        }
    }
}

