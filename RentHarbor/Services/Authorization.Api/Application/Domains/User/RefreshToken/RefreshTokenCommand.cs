using MediatR;

namespace Authorization.Application.Domains.User.RefreshToken
{
    public class RefreshTokenCommand : IRequest<RefreshTokenResult>
    {
        public string RefreshToken { get; set; }
    }
}
