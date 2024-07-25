using Xunit;
using FluentValidation.TestHelper;
using Ordering.Application.Domain.Order.GetRentalOfferById;

namespace Ordering.Unit.Tests.Application.Domain.Order.GetRentalOfferById
{
    public class GetRentalRequestQueryValidatorTests
    {
        private readonly GetRentalRequestQueryValidator _validator;

        public GetRentalRequestQueryValidatorTests()
        {
            _validator = new GetRentalRequestQueryValidator();
        }

        [Fact]
        public void Validator_ShouldHaveError_WhenRentalRequestIdIsZero()
        {
            var query = new GetRentalRequestQuery { OfferId = 0 };

            var result = _validator.TestValidate(query);

            result.ShouldHaveValidationErrorFor(q => q.OfferId);
        }

        [Fact]
        public void Validator_ShouldNotHaveError_WhenRentalRequestIdIsGreaterThanZero()
        {
            var query = new GetRentalRequestQuery { OfferId = 1 };

            var result = _validator.TestValidate(query);

            result.ShouldNotHaveValidationErrorFor(q => q.OfferId);
        }

        [Fact]
        public void Validator_ShouldHaveError_WhenOwnerIdIsEmpty()
        {
            var query = new GetRentalRequestQuery { OwnerId = "" };

            var result = _validator.TestValidate(query);

            result.ShouldHaveValidationErrorFor(q => q.OwnerId);
        }

        [Fact]
        public void Validator_ShouldNotHaveError_WhenOwnerIdIsNotEmpty()
        {
            var query = new GetRentalRequestQuery { OwnerId = Guid.NewGuid().ToString() };

            var result = _validator.TestValidate(query);

            result.ShouldNotHaveValidationErrorFor(q => q.OwnerId);
        }
    }
}

