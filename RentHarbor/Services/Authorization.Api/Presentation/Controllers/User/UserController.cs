using Authorization.Application.Domains.User.Register;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Authorization.Application.Domains.User.RefreshToken;
using System.Net;
using Authorization.Application.Domains.User.Login;
using Authorization.Infrastructure.Services.Jwt;
using Authorization.Persistance.Context;
using Microsoft.EntityFrameworkCore;

namespace Authorization.Api.Controllers.User
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IJwtService jwtService;
        private readonly AuthDbContext _authDbContext;

        public UserController(IMediator mediator, IJwtService jwtService, AuthDbContext authDbContext)
        {
            this.mediator = mediator;
            this.jwtService = jwtService;
            _authDbContext = authDbContext;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterCommand command)
        {
            var result = await mediator.Send(command);

            return Ok(new { Data = result, Status = HttpStatusCode.OK, Message = "User registered successfully." });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand command)
        {
            var result = await mediator.Send(command);

            HttpContext.Response.Headers.Add("Authorization", "Bearer " + result.AccessToken);
            return Ok(new { Data = result, Status = HttpStatusCode.OK, Message = "User registered successfully." });
        }


        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken()
        {
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
            string token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var userId = jwtService.GetPrincipalFromToken(token);

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            return Ok(new { UserId = userId });
        }

        [HttpGet("userName/{id}")]
        public async Task<IActionResult> GetUserNameById([FromRoute] string id)
        {
            var user = await _authDbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
            return Ok(user.UserName);
        }

        [HttpGet("userNameByToken")]
        public async Task<IActionResult> GetUserNameById()
        {
            string token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var userId = jwtService.GetPrincipalFromToken(token);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }
            var user = await _authDbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
            return Ok(new { UserName = user.UserName });
        }
    }
}
