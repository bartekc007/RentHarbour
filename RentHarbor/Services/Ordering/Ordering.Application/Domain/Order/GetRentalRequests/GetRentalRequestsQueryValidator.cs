using FluentValidation;

namespace Ordering.Application.Domain.Order.GetRentalRequests
{
    public class GetRentalRequestsQueryValidator : AbstractValidator<GetRentalRequestsQuery>
    {
        public GetRentalRequestsQueryValidator()
        {
            RuleFor(query => query.OwnerId)
                .NotEmpty().WithMessage("OwnerId cannot be empty.")
                .Length(36).WithMessage("OwnerId must be 36 characters long."); // Jeśli ID użytkownika jest UUID
        }
    }
}
