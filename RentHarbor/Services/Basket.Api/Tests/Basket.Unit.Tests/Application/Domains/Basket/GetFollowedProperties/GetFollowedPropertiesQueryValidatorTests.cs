using Xunit;
using FluentValidation.TestHelper;
using Basket.Application.Domains.Basket.GetFollowedProperties;

namespace Basket.Unit.Tests.Application.Domains.Basket.GetFollowedProperties
{
    public class GetFollowedPropertiesQueryValidatorTests
    {
        private readonly GetFollowedPropertiesQueryValidator _validator;

        public GetFollowedPropertiesQueryValidatorTests()
        {
            _validator = new GetFollowedPropertiesQueryValidator();
        }

        [Fact]
        public void Validate_ShouldHaveNoErrors_WhenUserIdIsValid()
        {
            // Arrange
            var query = new GetFollowedPropertiesQuery { UserId = "valid_user_id" };

            // Act
            var result = _validator.TestValidate(query);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void Validate_ShouldHaveErrors_WhenUserIdIsEmpty()
        {
            // Arrange
            var query = new GetFollowedPropertiesQuery { UserId = string.Empty };

            // Act
            var result = _validator.TestValidate(query);

            // Assert
            result.ShouldHaveValidationErrorFor(q => q.UserId).WithErrorMessage("UserId is required.");
        }
    }
}

