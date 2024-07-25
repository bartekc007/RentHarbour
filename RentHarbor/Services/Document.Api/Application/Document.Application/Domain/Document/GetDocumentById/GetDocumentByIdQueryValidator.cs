using FluentValidation;

namespace Document.Application.Domain.Document.GetDocumentById
{
    public class GetDocumentByIdQueryValidator : AbstractValidator<GetDocumentByIdQuery>
    {
        public GetDocumentByIdQueryValidator()
        {
            RuleFor(query => query.DocumentId)
                .NotEmpty().WithMessage("DocumentId cannot be empty.")
                .Matches("^[a-zA-Z0-9-]+$").WithMessage("DocumentId contains invalid characters.");
        }
    }
}
