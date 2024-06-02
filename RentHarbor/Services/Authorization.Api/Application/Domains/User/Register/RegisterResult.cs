namespace Authorization.Application.Domains.User.Register
{
    public class RegisterResult
    {
        public string Username { get; set; }
        public string AccessToken { get; set; }
        public string ReffreshToken { get; set; }
    }
}
