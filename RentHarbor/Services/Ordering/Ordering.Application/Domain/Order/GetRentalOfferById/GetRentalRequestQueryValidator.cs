using FluentValidation;

namespace Ordering.Application.Domain.Order.GetRentalOfferById
{
    public class GetRentalRequestQueryValidator : AbstractValidator<GetRentalRequestQuery>
    {
        public GetRentalRequestQueryValidator()
        {
            RuleFor(query => query.OwnerId)
                .NotEmpty().WithMessage("OwnerId cannot be empty.");

            RuleFor(query => query.OfferId)
                .GreaterThan(0).WithMessage("OfferId must be greater than 0.");
        }
    }
}
