using FluentValidation;

namespace Authorization.Application.Domains.User.RefreshToken
{
    public class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommand>
    {
        public RefreshTokenCommandValidator()
        {
            RuleFor(x => x.RefreshToken)
                .NotEmpty().WithMessage("Refresh token is required.")
                .Length(12, 256).WithMessage("Refresh token length must be between 32 and 256 characters.");
        }
    }
}
