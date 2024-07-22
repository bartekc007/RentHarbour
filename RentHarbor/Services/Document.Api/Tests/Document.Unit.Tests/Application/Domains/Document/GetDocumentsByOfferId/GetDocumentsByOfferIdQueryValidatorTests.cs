using FluentValidation.TestHelper;
using Document.Application.Domain.Document.GetDocumentsByOfferId;

namespace Document.Unit.Tests.Application.Domains.Document.GetDocumentsByOfferId
{
    public class GetDocumentsByOfferIdQueryValidatorTests
    {
        private readonly GetDocumentsByOfferIdQueryValidator _validator;

        public GetDocumentsByOfferIdQueryValidatorTests()
        {
            _validator = new GetDocumentsByOfferIdQueryValidator();
        }

        [Fact]
        public void Validator_ShouldHaveError_WhenOfferIdIsZeroOrNegative()
        {
            var query = new GetDocumentsByOfferIdQuery
            {
                OfferId = 0
            };

            var result = _validator.TestValidate(query);

            result.ShouldHaveValidationErrorFor(q => q.OfferId);

            query = new GetDocumentsByOfferIdQuery
            {
                OfferId = -1
            };

            result = _validator.TestValidate(query);

            result.ShouldHaveValidationErrorFor(q => q.OfferId);
        }

        [Fact]
        public void Validator_ShouldNotHaveError_WhenOfferIdIsValid()
        {
            var query = new GetDocumentsByOfferIdQuery
            {
                OfferId = 1,
                UserId = Guid.NewGuid().ToString()
            };

            var result = _validator.TestValidate(query);

            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}

