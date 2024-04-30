using Authorization.Application.Domains.User.Register;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Authorization.Application.Domains.User.RefreshToken;
using System.Net;
using Authorization.Application.Domains.User.Login;
using Microsoft.AspNetCore.Authorization;
using Authorization.Infrastructure.Services.Jwt;

namespace Authorization.Api.Controllers.User
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController: ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IJwtService jwtService;

        public UserController(IMediator mediator, IJwtService jwtService)
        {
            this.mediator = mediator;
            this.jwtService = jwtService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]RegisterCommand command)
        {
            var result = await mediator.Send(command);
            
            // Utwórz odpowiedź HTTP z nagłówkiem zawierającym token JWT
            HttpContext.Response.Headers.Add("Authorization", "Bearer " + result.Token);
            return Ok(new { Status = HttpStatusCode.OK, Message = "User registered successfully." });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]LoginCommand command)
        {
            var result = await mediator.Send(command);

            // Utwórz odpowiedź HTTP z nagłówkiem zawierającym token JWT
            HttpContext.Response.Headers.Add("Authorization", "Bearer " + result.AccessToken);
            return Ok(new { Status = HttpStatusCode.OK, Message = "User registered successfully." });
        }


        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken()
        {
            // Pobierz token odświeżania z nagłówka Authorization
            string refreshToken = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            var command = new RefreshTokenCommand
            {
                RefreshToken = refreshToken
            };

            var result = await mediator.Send(command);

            HttpContext.Response.Headers.Add("Authorization", "Bearer " + result.AccessToken);
            return Ok(new { Status = HttpStatusCode.OK, Message = "User registered successfully." });
        }

        [HttpGet("userId")]
        public async Task<IActionResult> GetUserIdFromTokenAsync()
        {
            // Pobierz obecne żądanie
            string token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var userId = jwtService.GetPrincipalFromToken(token);

            if (string.IsNullOrEmpty(userId))
            {
                // Użytkownik nie jest uwierzytelniony lub nie ma wymaganego tokenu
                return Unauthorized();
            }

            return Ok(new { UserId = userId });
        }
    }
}
