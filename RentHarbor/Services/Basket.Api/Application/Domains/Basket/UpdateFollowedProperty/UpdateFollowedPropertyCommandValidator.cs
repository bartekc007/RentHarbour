using FluentValidation;

namespace Basket.Application.Domains.Basket.UpdateFollowedProperty
{
    public class UpdateFollowedPropertyCommandValidator : AbstractValidator<UpdateFollowedPropertyCommand>
    {
        public UpdateFollowedPropertyCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id is required.")
                .Length(36).WithMessage("Id must be 36 characters long.");

            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("UserId is required.")
                .Length(36).WithMessage("UserId must be 36 characters long.");

            RuleFor(x => x.PropertyId)
                .NotEmpty().WithMessage("PropertyId is required.")
                .Length(36).WithMessage("PropertyId must be 36 characters long.");

            RuleFor(x => x.Action)
                .IsInEnum().WithMessage("Action must be a valid value.");
        }
    }
}
