using Xunit;
using FluentValidation.TestHelper;
using Document.Application.Domain.Document.GetDocumentById;

namespace Document.Unit.Tests.Application.Domains.Document.GetDocumentById
{
    public class GetDocumentByIdQueryValidatorTests
    {
        private readonly GetDocumentByIdQueryValidator _validator;

        public GetDocumentByIdQueryValidatorTests()
        {
            _validator = new GetDocumentByIdQueryValidator();
        }

        [Fact]
        public void Validator_ShouldHaveError_WhenDocumentIdIsEmpty()
        {
            var query = new GetDocumentByIdQuery
            {
                DocumentId = ""
            };

            var result = _validator.TestValidate(query);

            result.ShouldHaveValidationErrorFor(q => q.DocumentId);
        }

        [Fact]
        public void Validator_ShouldHaveError_WhenDocumentIdContainsInvalidCharacters()
        {
            var query = new GetDocumentByIdQuery
            {
                DocumentId = "5f47ac7b!452ef001d4fb9c5"
            };

            var result = _validator.TestValidate(query);

            result.ShouldHaveValidationErrorFor(q => q.DocumentId);
        }

        [Fact]
        public void Validator_ShouldNotHaveError_WhenDocumentIdIsValid()
        {
            var query = new GetDocumentByIdQuery
            {
                DocumentId = "5f47ac7b8452ef001d4fb9c5"
            };

            var result = _validator.TestValidate(query);

            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}

