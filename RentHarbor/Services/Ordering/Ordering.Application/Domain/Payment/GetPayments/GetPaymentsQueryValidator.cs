using FluentValidation;

namespace Ordering.Application.Domain.Payment.GetPayments
{
    public class GetPaymentsQueryValidator : AbstractValidator<GetPaymentsQuery>
    {
        public GetPaymentsQueryValidator()
        {
            RuleFor(query => query.UserId)
                .NotEmpty().WithMessage("UserId cannot be empty.")
                .Length(36).WithMessage("UserId must be 36 characters long."); // Jeśli ID użytkownika jest UUID

            RuleFor(query => query.PropertyId)
                .NotEmpty().WithMessage("PropertyId cannot be empty.")
                .Length(36).WithMessage("PropertyId must be 36 characters long."); // Jeśli ID nieruchomości jest UUID
        }
    }
}
