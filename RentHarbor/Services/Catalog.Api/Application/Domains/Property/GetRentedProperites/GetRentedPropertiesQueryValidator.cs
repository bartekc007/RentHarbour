using FluentValidation;

namespace Catalog.Application.Domains.Property.GetRentedProperites
{
    public class GetRentedPropertiesQueryValidator : AbstractValidator<GetRentedPropertiesQuery>
    {
        public GetRentedPropertiesQueryValidator()
        {
            RuleFor(query => query.UserId)
                .NotEmpty().WithMessage("UserId cannot be empty.")
                .Matches("^[a-zA-Z0-9-]+$").WithMessage("UserId contains invalid characters."); // Adjust regex as needed
        }
    }
}
