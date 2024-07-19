using FluentValidation;

namespace Ordering.Application.Domain.Order.AcceptRentalRequest
{
    public class AcceptRentalRequestCommandValidator : AbstractValidator<AcceptRentalRequestCommand>
    {
        public AcceptRentalRequestCommandValidator()
        {
            RuleFor(command => command.OfferId)
                .GreaterThan(0).WithMessage("OfferId must be greater than 0.");

            RuleFor(command => command.UserId)
                .NotEmpty().WithMessage("UserId cannot be empty.");

            RuleFor(command => command.Status)
                .IsInEnum().WithMessage("Status must be a valid value from RentalRequestStatus enum.");
        }
    }
}
