using FluentValidation.TestHelper;
using Document.Application.Domain.Document.AddDocument;
using Microsoft.AspNetCore.Http;

namespace Document.Unit.Tests.Application.Domains.Document.AddDocument
{
    public class AddDocumentCommandValidatorTests
    {
        private readonly AddDocumentCommandValidator _validator;

        public AddDocumentCommandValidatorTests()
        {
            _validator = new AddDocumentCommandValidator();
        }

        [Fact]
        public void Validator_ShouldHaveError_WhenOfferIdIsInvalid()
        {
            var command = new AddDocumentCommand
            {
                OfferId = 0,
                UserId = "user123",
                File = new FormFile(new MemoryStream(), 0, 0, "Data", "test.pdf")
            };

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(c => c.OfferId);
        }

        [Fact]
        public void Validator_ShouldHaveError_WhenUserIdIsEmpty()
        {
            var command = new AddDocumentCommand
            {
                OfferId = 1,
                UserId = "",
                File = new FormFile(new MemoryStream(), 0, 0, "Data", "test.pdf")
            };

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(c => c.UserId);
        }

        [Fact]
        public void Validator_ShouldHaveError_WhenUserIdIsInvalid()
        {
            var command = new AddDocumentCommand
            {
                OfferId = 1,
                UserId = "invalid_user_id",
                File = new FormFile(new MemoryStream(), 0, 0, "Data", "test.pdf")
            };

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(c => c.UserId);
        }

        [Fact]
        public void Validator_ShouldNotHaveError_WhenCommandIsValid()
        {
            var command = new AddDocumentCommand
            {
                OfferId = 1,
                UserId = "user123",
                File = new FormFile(new MemoryStream(), 0, 0, "Data", "test.pdf")
            };

            var result = _validator.TestValidate(command);

            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}

