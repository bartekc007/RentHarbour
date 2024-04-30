using Authorization.Persistance.Entities;
using System.Security.Claims;

namespace Authorization.Infrastructure.Services.Jwt
{
    public interface IJwtService
    {
        string GetPrincipalFromToken(string token);
        string GenerateAccessToken(ApplicationUser user);
        string GenerateRefreshToken();
        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
