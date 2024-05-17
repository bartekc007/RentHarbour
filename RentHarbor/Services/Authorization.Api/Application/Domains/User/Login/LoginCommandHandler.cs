using Authorization.Infrastructure.Services.Jwt;
using Authorization.Persistance.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Authorization.Application.Domains.User.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResult>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtService _jwtService;

        public LoginCommandHandler(UserManager<ApplicationUser> userManager, IJwtService jwtService)
        {
            _userManager = userManager;
            _jwtService = jwtService;
        }

        public async Task<LoginResult> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            // Sprawdź, czy istnieje użytkownik o podanej nazwie użytkownika
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null)
            {
                // Obsłuż brak użytkownika
                // Możesz rzucić wyjątek lub zwrócić odpowiedni wynik
                throw new ApplicationException("Invalid username or password.");
            }

            // Sprawdź, czy hasło jest poprawne
            var signInResult = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!signInResult)
            {
                // Obsłuż nieprawidłowe hasło
                // Możesz rzucić wyjątek lub zwrócić odpowiedni wynik
                throw new ApplicationException("Invalid username or password.");
            }

            // Wygeneruj tokeny JWT
            var accessToken = _jwtService.GenerateAccessToken(user);
            var refreshToken = _jwtService.GenerateRefreshToken();

            // Zapisz refreshToken w bazie danych (opcjonalne)

            return new LoginResult
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }
    }
}

// C:\Users\barte\.aspnet\https
