using FluentValidation.TestHelper;
using Basket.Application.Domains.Basket.UpdateBasket;

namespace Basket.Unit.Tests.Application.Domains.Basket.UpdateBasket
{
    public class UpdateBasketCommandValidatorTests
    {
        private readonly UpdateBasketCommandValidator _validator;

        public UpdateBasketCommandValidatorTests()
        {
            _validator = new UpdateBasketCommandValidator();
        }

        [Fact]
        public void Validate_ShouldHaveNoErrors_WhenCommandIsValid()
        {
            var command = new UpdateBasketCommand
            {
                UserId = "valid_user_id",
                PropertyId = "valid_property_id",
                Name = "Valid Name",
                Description = "Valid Description",
                AddressStreet = "Valid Street",
                AddressCity = "Valid City",
                AddressState = "Valid State",
                AddressPostalCode = "12345",
                AddressCountry = "Valid Country",
                Price = 100.0,
                Bedrooms = 2,
                Bathrooms = 1,
                AreaSquareMeters = 50,
                IsAvailable = true
            };

            var result = _validator.TestValidate(command);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void Validate_ShouldHaveErrors_WhenUserIdIsEmpty()
        {
            var command = new UpdateBasketCommand { UserId = string.Empty };

            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(c => c.UserId).WithErrorMessage("UserId is required.");
        }
    }
}

