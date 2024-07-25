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

            var result = _validator.TestValidate(query);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void Validate_ShouldHaveErrors_WhenPriceMinIsNegative()
        {
            var query = new GetPropertiesQuery { PriceMin = -1 };

            var result = _validator.TestValidate(query);

            result.ShouldHaveValidationErrorFor(q => q.PriceMin).WithErrorMessage("PriceMin must be greater than or equal to 0.");
        }

        [Fact]
        public void Validate_ShouldHaveErrors_WhenPriceMaxIsNegative()
        {
            var query = new GetPropertiesQuery { PriceMax = -1 };

            var result = _validator.TestValidate(query);

            result.ShouldHaveValidationErrorFor(q => q.PriceMax).WithErrorMessage("PriceMax must be greater than or equal to 0.");
        }

        [Fact]
        public void Validate_ShouldHaveErrors_WhenBedroomsMinIsNegative()
        {
            var query = new GetPropertiesQuery { BedroomsMin = -1 };

            var result = _validator.TestValidate(query);

            result.ShouldHaveValidationErrorFor(q => q.BedroomsMin).WithErrorMessage("BedroomsMin must be greater than or equal to 0.");
        }

        [Fact]
        public void Validate_ShouldHaveErrors_WhenBedroomsMaxIsNegative()
        {
            var query = new GetPropertiesQuery { BedroomsMax = -1 };

            var result = _validator.TestValidate(query);

            result.ShouldHaveValidationErrorFor(q => q.BedroomsMax).WithErrorMessage("BedroomsMax must be greater than or equal to 0.");
        }

        [Fact]
        public void Validate_ShouldHaveErrors_WhenBathroomsMinIsNegative()
        {
            var query = new GetPropertiesQuery { BathroomsMin = -1 };

            var result = _validator.TestValidate(query);

            result.ShouldHaveValidationErrorFor(q => q.BathroomsMin).WithErrorMessage("BathroomsMin must be greater than or equal to 0.");
        }

        [Fact]
        public void Validate_ShouldHaveErrors_WhenBathroomsMaxIsNegative()
        {
            var query = new GetPropertiesQuery { BathroomsMax = -1 };

            var result = _validator.TestValidate(query);

            result.ShouldHaveValidationErrorFor(q => q.BathroomsMax).WithErrorMessage("BathroomsMax must be greater than or equal to 0.");
        }

        [Fact]
        public void Validate_ShouldHaveErrors_WhenAreaSquareMetersMinIsNegative()
        {
            var query = new GetPropertiesQuery { AreaSquareMetersMin = -1 };

            var result = _validator.TestValidate(query);

            result.ShouldHaveValidationErrorFor(q => q.AreaSquareMetersMin).WithErrorMessage("AreaSquareMetersMin must be greater than or equal to 0.");
        }

        [Fact]
        public void Validate_ShouldHaveErrors_WhenAreaSquareMetersMaxIsNegative()
        {
            var query = new GetPropertiesQuery { AreaSquareMetersMax = -1 };

            var result = _validator.TestValidate(query);

            result.ShouldHaveValidationErrorFor(q => q.AreaSquareMetersMax).WithErrorMessage("AreaSquareMetersMax must be greater than or equal to 0.");
        }

        [Fact]
        public void Validate_ShouldHaveNoErrors_WhenOptionalFieldsAreNotSet()
        {
            var query = new GetPropertiesQuery();

            var result = _validator.TestValidate(query);

            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}

