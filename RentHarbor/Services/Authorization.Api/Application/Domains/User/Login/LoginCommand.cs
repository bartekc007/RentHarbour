using MediatR;

namespace Authorization.Application.Domains.User.Login
{
    public class LoginCommand : IRequest<LoginResult>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
