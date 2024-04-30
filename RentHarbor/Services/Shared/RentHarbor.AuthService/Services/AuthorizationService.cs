using Flurl;
using Flurl.Http;

namespace RentHarbor.AuthService.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly string _authorizationBaseUrl = "http://authorization.api:8002/api/User/";

        public async Task<string> GetUserIdFromTokenAsync(string jwtToken)
        {
            try
            {
                // Wysyłamy żądanie HTTP do Mikroserwisu Authorization, aby uzyskać Id użytkownika
                var response = await _authorizationBaseUrl
                    .AppendPathSegment("userId")
                    .WithOAuthBearerToken(jwtToken) // Przekaż token JWT w nagłówku uwierzytelniającym
                    .GetAsync();

                // Sprawdzamy, czy odpowiedź jest udana


                // Odczytujemy Id użytkownika z odpowiedzi
                var userId = await response.GetStringAsync();
                return userId;
            }
            catch (FlurlHttpException ex)
            {
                // Obsługa błędów z Flurl
                // Możesz dostosować obsługę błędów zgodnie z Twoimi wymaganiami
                throw new Exception("Błąd podczas wysyłania żądania HTTP do Mikroserwisu Authorization.", ex);
            }
        }
    }
}
