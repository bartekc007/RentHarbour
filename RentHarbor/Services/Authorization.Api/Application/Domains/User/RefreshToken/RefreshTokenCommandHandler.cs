using Authorization.Infrastructure.Services.Jwt;
using Authorization.Persistance.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Authorization.Application.Domains.User.RefreshToken
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, RefreshTokenResult>
    {
        private readonly IJwtService _jwtService;
        private readonly UserManager<ApplicationUser> _userManager;

        public RefreshTokenCommandHandler(IJwtService jwtService, UserManager<ApplicationUser> userManager)
        {
            _jwtService = jwtService;
            _userManager = userManager;
        }

        public async Task<RefreshTokenResult> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var principal = _jwtService.GetPrincipalFromExpiredToken(request.RefreshToken);
            var userIdClaim = principal.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return RefreshTokenResult.Fail("Invalid token");
            }

            var user = await _userManager.FindByIdAsync(userIdClaim.Value);
            if (user == null)
            {
                return RefreshTokenResult.Fail("User not found");
            }

            var accessToken = _jwtService.GenerateAccessToken(user);
            var newRefreshToken = _jwtService.GenerateRefreshToken();

            // Aktualizuj token odświeżania w bazie danych
            user.RefreshToken = newRefreshToken;
            await _userManager.UpdateAsync(user);

            return RefreshTokenResult.Ok(accessToken, newRefreshToken);
        }
    }
}
