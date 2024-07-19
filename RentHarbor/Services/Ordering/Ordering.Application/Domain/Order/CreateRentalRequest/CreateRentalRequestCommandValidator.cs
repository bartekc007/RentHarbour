using FluentValidation;

namespace Ordering.Application.Domain.Order.CreateRentalRequest
{
    public class CreateRentalRequestCommandValidator : AbstractValidator<CreateRentalRequestCommand>
    {
        public CreateRentalRequestCommandValidator()
        {
            RuleFor(command => command.PropertyId)
                .NotEmpty().WithMessage("PropertyId cannot be empty.");

            RuleFor(command => command.UserId)
                .NotEmpty().WithMessage("UserId cannot be empty.");

            RuleFor(command => command.StartDate)
                .GreaterThan(DateTime.Now).WithMessage("StartDate must be in the future.");

            RuleFor(command => command.EndDate)
                .GreaterThan(command => command.StartDate)
                .WithMessage("EndDate must be after StartDate.");

            RuleFor(command => command.NumberOfPeople)
                .GreaterThan(0).WithMessage("NumberOfPeople must be greater than 0.");

            RuleFor(command => command.MessageToOwner)
                .MaximumLength(1000).WithMessage("MessageToOwner cannot be longer than 1000 characters.");
        }
    }
}
