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
            var query = new GetFollowedPropertiesQuery { UserId = "valid_user_id" };

            var result = _validator.TestValidate(query);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void Validate_ShouldHaveErrors_WhenUserIdIsEmpty()
        {
            var query = new GetFollowedPropertiesQuery { UserId = string.Empty };

            var result = _validator.TestValidate(query);

            result.ShouldHaveValidationErrorFor(q => q.UserId).WithErrorMessage("UserId is required.");
        }
    }
}

