using FluentValidation.TestHelper;
using Catalog.Application.Domains.Property.GetProperties;

namespace Catalog.Unit.Tests.Application.Domains.GetProperties
{
    public class GetPropertiesQueryValidatorTests
    {
        private readonly GetPropertiesQueryValidator _validator;

        public GetPropertiesQueryValidatorTests()
        {
            _validator = new GetPropertiesQueryValidator();
        }

        [Fact]
        public void Validate_ShouldHaveNoErrors_WhenQueryIsValid()
        {
            // Arrange
            var query = new GetPropertiesQuery
            {
                PriceMin = 100,
                PriceMax = 500,
                BedroomsMin = 1,
                BedroomsMax = 3,
                BathroomsMin = 1,
                BathroomsMax = 2,
                AreaSquareMetersMin = 50,
                AreaSquareMetersMax = 150,
                City = "SampleCity"
            };

            // Act
            var result = _validator.TestValidate(query);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void Validate_ShouldHaveErrors_WhenPriceMinIsNegative()
        {
            // Arrange
            var query = new GetPropertiesQuery { PriceMin = -1 };

            // Act
            var result = _validator.TestValidate(query);

            // Assert
            result.ShouldHaveValidationErrorFor(q => q.PriceMin).WithErrorMessage("PriceMin must be greater than or equal to 0.");
        }

        [Fact]
        public void Validate_ShouldHaveErrors_WhenPriceMaxIsNegative()
        {
            // Arrange
            var query = new GetPropertiesQuery { PriceMax = -1 };

            // Act
            var result = _validator.TestValidate(query);

            // Assert
            result.ShouldHaveValidationErrorFor(q => q.PriceMax).WithErrorMessage("PriceMax must be greater than or equal to 0.");
        }

        [Fact]
        public void Validate_ShouldHaveErrors_WhenBedroomsMinIsNegative()
        {
            // Arrange
            var query = new GetPropertiesQuery { BedroomsMin = -1 };

            // Act
            var result = _validator.TestValidate(query);

            // Assert
            result.ShouldHaveValidationErrorFor(q => q.BedroomsMin).WithErrorMessage("BedroomsMin must be greater than or equal to 0.");
        }

        [Fact]
        public void Validate_ShouldHaveErrors_WhenBedroomsMaxIsNegative()
        {
            // Arrange
            var query = new GetPropertiesQuery { BedroomsMax = -1 };

            // Act
            var result = _validator.TestValidate(query);

            // Assert
            result.ShouldHaveValidationErrorFor(q => q.BedroomsMax).WithErrorMessage("BedroomsMax must be greater than or equal to 0.");
        }

        [Fact]
        public void Validate_ShouldHaveErrors_WhenBathroomsMinIsNegative()
        {
            // Arrange
            var query = new GetPropertiesQuery { BathroomsMin = -1 };

            // Act
            var result = _validator.TestValidate(query);

            // Assert
            result.ShouldHaveValidationErrorFor(q => q.BathroomsMin).WithErrorMessage("BathroomsMin must be greater than or equal to 0.");
        }

        [Fact]
        public void Validate_ShouldHaveErrors_WhenBathroomsMaxIsNegative()
        {
            // Arrange
            var query = new GetPropertiesQuery { BathroomsMax = -1 };

            // Act
            var result = _validator.TestValidate(query);

            // Assert
            result.ShouldHaveValidationErrorFor(q => q.BathroomsMax).WithErrorMessage("BathroomsMax must be greater than or equal to 0.");
        }

        [Fact]
        public void Validate_ShouldHaveErrors_WhenAreaSquareMetersMinIsNegative()
        {
            // Arrange
            var query = new GetPropertiesQuery { AreaSquareMetersMin = -1 };

            // Act
            var result = _validator.TestValidate(query);

            // Assert
            result.ShouldHaveValidationErrorFor(q => q.AreaSquareMetersMin).WithErrorMessage("AreaSquareMetersMin must be greater than or equal to 0.");
        }

        [Fact]
        public void Validate_ShouldHaveErrors_WhenAreaSquareMetersMaxIsNegative()
        {
            // Arrange
            var query = new GetPropertiesQuery { AreaSquareMetersMax = -1 };

            // Act
            var result = _validator.TestValidate(query);

            // Assert
            result.ShouldHaveValidationErrorFor(q => q.AreaSquareMetersMax).WithErrorMessage("AreaSquareMetersMax must be greater than or equal to 0.");
        }

        [Fact]
        public void Validate_ShouldHaveNoErrors_WhenOptionalFieldsAreNotSet()
        {
            // Arrange
            var query = new GetPropertiesQuery();

            // Act
            var result = _validator.TestValidate(query);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}

