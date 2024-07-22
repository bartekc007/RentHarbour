using FluentValidation;

namespace Catalog.Application.Domains.Property.GetProperties
{
    public class GetPropertiesQueryValidator : AbstractValidator<GetPropertiesQuery>
    {
        public GetPropertiesQueryValidator()
        {
            RuleFor(x => x.PriceMax)
                .GreaterThanOrEqualTo(0).When(x => x.PriceMax.HasValue).WithMessage("PriceMax must be greater than or equal to 0.");

            RuleFor(x => x.PriceMin)
                .GreaterThanOrEqualTo(0).When(x => x.PriceMin.HasValue).WithMessage("PriceMin must be greater than or equal to 0.");

            RuleFor(x => x.BedroomsMax)
                .GreaterThanOrEqualTo(0).When(x => x.BedroomsMax.HasValue).WithMessage("BedroomsMax must be greater than or equal to 0.");

            RuleFor(x => x.BedroomsMin)
                .GreaterThanOrEqualTo(0).When(x => x.BedroomsMin.HasValue).WithMessage("BedroomsMin must be greater than or equal to 0.");

            RuleFor(x => x.BathroomsMax)
                .GreaterThanOrEqualTo(0).When(x => x.BathroomsMax.HasValue).WithMessage("BathroomsMax must be greater than or equal to 0.");

            RuleFor(x => x.BathroomsMin)
                .GreaterThanOrEqualTo(0).When(x => x.BathroomsMin.HasValue).WithMessage("BathroomsMin must be greater than or equal to 0.");

            RuleFor(x => x.AreaSquareMetersMax)
                .GreaterThanOrEqualTo(0).When(x => x.AreaSquareMetersMax.HasValue).WithMessage("AreaSquareMetersMax must be greater than or equal to 0.");

            RuleFor(x => x.AreaSquareMetersMin)
                .GreaterThanOrEqualTo(0).When(x => x.AreaSquareMetersMin.HasValue).WithMessage("AreaSquareMetersMin must be greater than or equal to 0.");

            RuleFor(x => x)
                .Custom((query, context) =>
                {
                    if (query.PriceMin.HasValue && query.PriceMax.HasValue && query.PriceMin > query.PriceMax)
                    {
                        context.AddFailure("PriceMin", "PriceMin cannot be greater than PriceMax.");
                    }

                    if (query.BedroomsMin.HasValue && query.BedroomsMax.HasValue && query.BedroomsMin > query.BedroomsMax)
                    {
                        context.AddFailure("BedroomsMin", "BedroomsMin cannot be greater than BedroomsMax.");
                    }

                    if (query.BathroomsMin.HasValue && query.BathroomsMax.HasValue && query.BathroomsMin > query.BathroomsMax)
                    {
                        context.AddFailure("BathroomsMin", "BathroomsMin cannot be greater than BathroomsMax.");
                    }

                    if (query.AreaSquareMetersMin.HasValue && query.AreaSquareMetersMax.HasValue && query.AreaSquareMetersMin > query.AreaSquareMetersMax)
                    {
                        context.AddFailure("AreaSquareMetersMin", "AreaSquareMetersMin cannot be greater than AreaSquareMetersMax.");
                    }
                });
        }
    }
}
