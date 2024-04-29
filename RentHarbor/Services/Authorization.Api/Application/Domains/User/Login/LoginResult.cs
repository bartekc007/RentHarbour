namespace Authorization.Application.Domains.User.Login
{
    public class LoginResult
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
