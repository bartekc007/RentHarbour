using Flurl;
using Flurl.Http;

namespace RentHarbor.AuthService.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly string _authorizationBaseUrl = "http://authorization.api/api/User/";

        public async Task<string> GetUserIdFromTokenAsync(string jwtToken)
        {
            try
            {
                var response = await _authorizationBaseUrl
                    .AppendPathSegment("userId")
                    .WithOAuthBearerToken(jwtToken)
                    .GetAsync();

                var userId = await response.GetStringAsync();
                return userId;
            }
            catch (FlurlHttpException ex)
            {
                throw new Exception("Błąd podczas wysyłania żądania HTTP do Mikroserwisu Authorization.", ex);
            }
        }

        public async Task<string> GetUserNameById(string id)
        {
            try
            {
                var response = await _authorizationBaseUrl
                    .AppendPathSegment($"userName/{id}")
                    .GetAsync();

                var userName = await response.GetStringAsync();
                return userName;
            }
            catch (FlurlHttpException ex)
            {
                throw new Exception("Błąd podczas wysyłania żądania HTTP do Mikroserwisu Authorization.", ex);
            }
        }

    }
}
