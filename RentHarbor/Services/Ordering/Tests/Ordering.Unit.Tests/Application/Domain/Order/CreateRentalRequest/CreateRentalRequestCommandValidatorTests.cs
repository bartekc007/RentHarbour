using FluentValidation.TestHelper;
using Ordering.Application.Domain.Order.CreateRentalRequest;

namespace Ordering.Unit.Tests.Application.Domain.Order.CreateRentalRequest
{
    public class CreateRentalRequestCommandValidatorTests
    {
        private readonly CreateRentalRequestCommandValidator _validator;

        public CreateRentalRequestCommandValidatorTests()
        {
            _validator = new CreateRentalRequestCommandValidator();
        }

        [Fact]
        public void Validator_ShouldHaveError_WhenPropertyIdisEmpty()
        {
            var command = new CreateRentalRequestCommand { PropertyId = "" };

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(c => c.PropertyId);
        }

        [Fact]
        public void Validator_ShouldHaveError_WhenUserIdIsEmpty()
        {
            var command = new CreateRentalRequestCommand { UserId = "" };

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(c => c.UserId);
        }

        [Fact]
        public void Validator_ShouldNotHaveError_WhenUserIdIsValid()
        {
            var command = new CreateRentalRequestCommand { UserId = "valid-user-id" };

            var result = _validator.TestValidate(command);

            result.ShouldNotHaveValidationErrorFor(c => c.UserId);
        }

        [Fact]
        public void Validator_ShouldHaveError_WhenStartDateIsInPast()
        {
            var command = new CreateRentalRequestCommand { StartDate = DateTime.Now.AddDays(-1) };

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(c => c.StartDate);
        }

        [Fact]
        public void Validator_ShouldHaveError_WhenEndDateIsBeforeStartDate()
        {
            var command = new CreateRentalRequestCommand { StartDate = DateTime.Now.AddDays(2), EndDate = DateTime.Now.AddDays(1) };

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(c => c.EndDate);
        }

        [Fact]
        public void Validator_ShouldNotHaveError_WhenDatesAreValid()
        {
            var command = new CreateRentalRequestCommand { StartDate = DateTime.Now.AddDays(1), EndDate = DateTime.Now.AddDays(2) };

            var result = _validator.TestValidate(command);

            result.ShouldNotHaveValidationErrorFor(c => c.StartDate);
            result.ShouldNotHaveValidationErrorFor(c => c.EndDate);
        }
    }
}
