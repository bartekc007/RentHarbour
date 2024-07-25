using FluentValidation.TestHelper;
using Basket.Application.Domains.Basket.UpdateFollowedProperty;

namespace Basket.Unit.Tests.Application.Domains.Basket.UpdateFollowedProperty
{
    public class UpdateFollowedPropertyCommandValidatorTests
    {
        private readonly UpdateFollowedPropertyCommandValidator _validator;

        public UpdateFollowedPropertyCommandValidatorTests()
        {
            _validator = new UpdateFollowedPropertyCommandValidator();
        }

        [Fact]
        public void Validate_ShouldHaveNoErrors_WhenCommandIsValid()
        {
            var command = new UpdateFollowedPropertyCommand
            {
                Id = "00000000-0000-0000-0000-000000000000",
                UserId = "00000000-0000-0000-0000-000000000000",
                PropertyId = "00000000-0000-0000-0000-000000000000",
                Action = UserPropertyAction.Add
            };

            var result = _validator.TestValidate(command);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void Validate_ShouldHaveErrors_WhenIdIsEmpty()
        {
            var command = new UpdateFollowedPropertyCommand
            {
                Id = string.Empty,
                UserId = "00000000-0000-0000-0000-000000000000",
                PropertyId = "00000000-0000-0000-0000-000000000000",
                Action = UserPropertyAction.Add
            };

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(c => c.Id).WithErrorMessage("Id is required.");
        }

        [Fact]
        public void Validate_ShouldHaveErrors_WhenUserIdIsEmpty()
        {
            var command = new UpdateFollowedPropertyCommand
            {
                Id = "00000000-0000-0000-0000-000000000000",
                UserId = string.Empty,
                PropertyId = "00000000-0000-0000-0000-000000000000",
                Action = UserPropertyAction.Add
            };

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(c => c.UserId).WithErrorMessage("UserId is required.");
        }

        [Fact]
        public void Validate_ShouldHaveErrors_WhenPropertyIdIsEmpty()
        {
            var command = new UpdateFollowedPropertyCommand
            {
                Id = "00000000-0000-0000-0000-000000000000",
                UserId = "00000000-0000-0000-0000-000000000000",
                PropertyId = string.Empty,
                Action = UserPropertyAction.Add
            };

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(c => c.PropertyId).WithErrorMessage("PropertyId is required.");
        }

        [Fact]
        public void Validate_ShouldHaveErrors_WhenActionIsInvalid()
        {
            var command = new UpdateFollowedPropertyCommand
            {
                Id = "00000000-0000-0000-0000-000000000000",
                UserId = "00000000-0000-0000-0000-000000000000",
                PropertyId = "00000000-0000-0000-0000-000000000000",
                Action = (UserPropertyAction)999
            };

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(c => c.Action).WithErrorMessage("Action must be a valid value.");
        }
    }
}

