using FluentValidation;

namespace Catalog.Application.Domains.Property.GetPropertyById
{
    public class GetPropertyByIdQueryValidator : AbstractValidator<GetPropertyByIdQuery>
    {
        public GetPropertyByIdQueryValidator()
        {
            RuleFor(query => query.PropertyId)
                .NotEmpty().WithMessage("PropertyId cannot be empty.")
                .Matches("^[a-zA-Z0-9-]+$").WithMessage("PropertyId contains invalid characters.");
        }
    }
}
