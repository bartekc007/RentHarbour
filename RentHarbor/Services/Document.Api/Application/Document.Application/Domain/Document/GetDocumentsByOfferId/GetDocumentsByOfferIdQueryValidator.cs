using FluentValidation;

namespace Document.Application.Domain.Document.GetDocumentsByOfferId
{
    public class GetDocumentsByOfferIdQueryValidator : AbstractValidator<GetDocumentsByOfferIdQuery>
    {
        public GetDocumentsByOfferIdQueryValidator()
        {
            RuleFor(query => query.OfferId)
                .GreaterThan(0).WithMessage("OfferId must be greater than 0.");

            RuleFor(query => query.UserId)
                .NotEmpty().WithMessage("UserId cannot be empty.")
                .Matches("^[a-zA-Z0-9-]+$").WithMessage("UserId contains invalid characters."); // Możesz dostosować regex do swojego formatu UserId
        }
    }
}
