using FluentValidation;

namespace Ordering.Application.Domain.Order.GetRentalRequestByUserId
{
    public class GetRentalRequestByUserIdQueryValidator : AbstractValidator<GetRentalRequestByUserIdQuery>
    {
        public GetRentalRequestByUserIdQueryValidator()
        {
            RuleFor(query => query.UserId)
                .NotEmpty().WithMessage("UserId cannot be empty.");
        }
    }
}
