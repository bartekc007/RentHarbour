using FluentValidation;

namespace Document.Application.Domain.Document.AddDocument
{
    public class AddDocumentCommandValidator : AbstractValidator<AddDocumentCommand>
    {
        public AddDocumentCommandValidator()
        {
            RuleFor(command => command.OfferId)
                .GreaterThan(0).WithMessage("OfferId must be greater than 0.");

            RuleFor(command => command.UserId)
                .NotEmpty().WithMessage("UserId cannot be empty.")
                .Matches("^[a-zA-Z0-9-]+$").WithMessage("UserId contains invalid characters.");
        }
    }
}
