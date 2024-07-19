using FluentValidation;

namespace Basket.Application.Domains.Basket.GetFollowedProperties
{
    public class GetFollowedPropertiesQueryValidator : AbstractValidator<GetFollowedPropertiesQuery>
    {
        public GetFollowedPropertiesQueryValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("UserId is required.");
        }
    }
}
