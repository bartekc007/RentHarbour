using Authorization.Infrastructure.Services.Jwt;
using Authorization.Persistance.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Authorization.Application.Domains.User.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisterResult>
    {
        private readonly IJwtService _jwtService;
        private readonly UserManager<ApplicationUser> _userManager;

        public RegisterCommandHandler(UserManager<ApplicationUser> userManager, IJwtService jwtService)
        {
            _userManager = userManager;
            _jwtService = jwtService;
        }
        public async Task<RegisterResult> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var user = new ApplicationUser
            {
                UserName = request.UserName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                DateOfBirth = request.DateOfBirth,
                Address = request.Address,
                City = request.City,
                Country = request.Country
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                // Wygeneruj token JWT
                var tokenString = _jwtService.GenerateAccessToken(user);

                // Zwróć token JWT
                return new RegisterResult
                {
                    UserName = request.UserName,
                    Token = tokenString
                };
            }
            else
            {
                // Obsłuż błąd tworzenia użytkownika
            }

            //return result.Succeeded;
            return new RegisterResult();
        }
    }
}
