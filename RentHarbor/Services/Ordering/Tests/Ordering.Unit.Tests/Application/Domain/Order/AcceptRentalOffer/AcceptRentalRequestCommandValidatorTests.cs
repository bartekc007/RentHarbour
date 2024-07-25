using Xunit;
using FluentValidation.TestHelper;
using Ordering.Application.Domain.Order.AcceptRentalRequest;

namespace Ordering.Unit.Tests.Application.Domain.Order.AcceptRentalOffer
{
    public class AcceptRentalRequestCommandValidatorTests
    {
        private readonly AcceptRentalRequestCommandValidator _validator;

        public AcceptRentalRequestCommandValidatorTests()
        {
            _validator = new AcceptRentalRequestCommandValidator();
        }

        [Fact]
        public void Validator_ShouldHaveError_WhenRentalRequestIdIsZero()
        {
            var command = new AcceptRentalRequestCommand { OfferId = 0 };

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(c => c.OfferId);
        }

        [Fact]
        public void Validator_ShouldNotHaveError_WhenRentalRequestIdIsGreaterThanZero()
        {
            var command = new AcceptRentalRequestCommand { OfferId = 1 };

            var result = _validator.TestValidate(command);

            result.ShouldNotHaveValidationErrorFor(c => c.OfferId);
        }

        [Fact]
        public void Validator_ShouldHaveError_WhenUserIdIsEmpty()
        {
            var command = new AcceptRentalRequestCommand { OfferId = 1, UserId = "" };

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(c => c.UserId);
        }

        [Fact]
        public void Validator_ShouldNotHaveError_WhenUserIdIsValid()
        {
            var command = new AcceptRentalRequestCommand { OfferId = 1, UserId = "valid-user-id" };

            var result = _validator.TestValidate(command);

            result.ShouldNotHaveValidationErrorFor(c => c.UserId);
        }
    }
}

