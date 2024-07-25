using Authorization.Infrastructure.Services.Jwt;
using Authorization.Persistance.Context;
using Authorization.Persistance.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Authorization.Application.Domains.User.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResult>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IJwtService _jwtService;
        private readonly AuthDbContext _authDbContext;

        public LoginCommandHandler(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IJwtService jwtService, AuthDbContext authDbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtService = jwtService;
            _authDbContext = authDbContext;
        }

        public async Task<LoginResult> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _authDbContext.Users.FirstOrDefaultAsync(u => u.UserName == request.UserName);

            if (user != null)
            {
                if (user.LockoutEnd != null && user.LockoutEnd > DateTime.Now)
                {
                    throw new ApplicationException("Account is locked. Please try again later.");
                }

                var isPasswordValid = await VerifyPasswordAsync(user, request.Password);

                if (!isPasswordValid)
                {
                    user.AccessFailedCount++;

                    if (user.AccessFailedCount >= 5)
                    {
                        user.LockoutEnd = DateTime.Now.AddMinutes(10); 
                    }

                    await _authDbContext.SaveChangesAsync();

                    throw new ApplicationException("Invalid username or password.");
                }

                user.AccessFailedCount = 0;
                user.LockoutEnd = null;
                await _authDbContext.SaveChangesAsync();

                var accessToken = _jwtService.GenerateAccessToken(user);
                var refreshToken = _jwtService.GenerateRefreshToken();


                return new LoginResult
                {
                    UserName = request.UserName,
                    AccessToken = accessToken,
                    RefreshToken = refreshToken
                };
            }
            else
            {
                throw new ApplicationException("Invalid username or password.");
            }
        }

        private async Task<bool> VerifyPasswordAsync(ApplicationUser user, string password)
        {
            var hasher = new PasswordHasher<ApplicationUser>();
            return hasher.VerifyHashedPassword(user, user.PasswordHash, password) == PasswordVerificationResult.Success;
        }
    }
}

