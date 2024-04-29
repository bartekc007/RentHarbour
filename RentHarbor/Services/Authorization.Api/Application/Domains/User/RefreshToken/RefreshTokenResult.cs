namespace Authorization.Application.Domains.User.RefreshToken
{
    public class RefreshTokenResult
    {
        public bool Success { get; private set; }
        public string AccessToken { get; private set; }
        public string NewRefreshToken { get; private set; }
        public string ErrorMessage { get; private set; }

        private RefreshTokenResult(bool success, string accessToken, string newRefreshToken, string errorMessage)
        {
            Success = success;
            AccessToken = accessToken;
            NewRefreshToken = newRefreshToken;
            ErrorMessage = errorMessage;
        }

        public static RefreshTokenResult Ok(string accessToken, string newRefreshToken)
        {
            return new RefreshTokenResult(true, accessToken, newRefreshToken, null);
        }

        public static RefreshTokenResult Fail(string errorMessage)
        {
            return new RefreshTokenResult(false, null, null, errorMessage);
        }
    }
}
