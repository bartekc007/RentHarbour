using Xunit;
using FluentValidation.TestHelper;
using Ordering.Application.Domain.Order.GetRentalRequestByUserId;

namespace Ordering.Unit.Tests.Application.Domain.Order.GetRentalRequestByUserId
{
    public class GetRentalRequestByUserIdQueryValidatorTests
    {
        private readonly GetRentalRequestByUserIdQueryValidator _validator;

        public GetRentalRequestByUserIdQueryValidatorTests()
        {
            _validator = new GetRentalRequestByUserIdQueryValidator();
        }

        [Fact]
        public void Validator_ShouldHaveError_WhenUserIdIsEmpty()
        {
            var query = new GetRentalRequestByUserIdQuery { UserId = "" };

            var result = _validator.TestValidate(query);

            result.ShouldHaveValidationErrorFor(q => q.UserId);
        }

        [Fact]
        public void Validator_ShouldNotHaveError_WhenUserIdIsValid()
        {
            var query = new GetRentalRequestByUserIdQuery { UserId = "valid-user-id" };

            var result = _validator.TestValidate(query);

            result.ShouldNotHaveValidationErrorFor(q => q.UserId);
        }
    }
}

