using FluentValidation;

namespace Basket.Application.Domains.Basket.UpdateBasket
{
    public class UpdateBasketCommandValidator : AbstractValidator<UpdateBasketCommand>
    {
        public UpdateBasketCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("UserId is required.")
                .MinimumLength(6).WithMessage("UserId must be 36 characters long.");

            RuleFor(x => x.PropertyId)
                .NotEmpty().WithMessage("PropertyId is required.")
                .MinimumLength(6).WithMessage("PropertyId must be 36 characters long.");

            RuleFor(x => x.Action)
                .IsInEnum().WithMessage("Action must be a valid value.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name must be less than 100 characters long.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(500).WithMessage("Description must be less than 500 characters long.");

            RuleFor(x => x.AddressStreet)
                .NotEmpty().WithMessage("AddressStreet is required.")
                .MaximumLength(200).WithMessage("AddressStreet must be less than 200 characters long.");

            RuleFor(x => x.AddressCity)
                .NotEmpty().WithMessage("AddressCity is required.")
                .MaximumLength(100).WithMessage("AddressCity must be less than 100 characters long.");

            RuleFor(x => x.AddressState)
                .NotEmpty().WithMessage("AddressState is required.")
                .MaximumLength(100).WithMessage("AddressState must be less than 100 characters long.");

            RuleFor(x => x.AddressPostalCode)
                .NotEmpty().WithMessage("AddressPostalCode is required.")
                .Matches(@"^\d{5}(-\d{4})?$").WithMessage("AddressPostalCode must be in the format '12345' or '12345-6789'.");

            RuleFor(x => x.AddressCountry)
                .NotEmpty().WithMessage("AddressCountry is required.")
                .MaximumLength(100).WithMessage("AddressCountry must be less than 100 characters long.");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Price must be greater than 0.");

            RuleFor(x => x.Bedrooms)
                .GreaterThanOrEqualTo(0).WithMessage("Bedrooms must be 0 or greater.");

            RuleFor(x => x.Bathrooms)
                .GreaterThanOrEqualTo(0).WithMessage("Bathrooms must be 0 or greater.");

            RuleFor(x => x.AreaSquareMeters)
                .GreaterThan(0).WithMessage("AreaSquareMeters must be greater than 0.");

            RuleFor(x => x.IsAvailable)
                .NotNull().WithMessage("IsAvailable is required.");
        }
    }
}
