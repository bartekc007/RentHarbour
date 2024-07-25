using Basket.Api.Requests;
using Basket.Application.Domains.Basket.GetFollowedProperties;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace Basket.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PropertyController : ControllerBase
    {
        public readonly RentHarbor.AuthService.Services.IAuthorizationService _authorizationService;
        public readonly IMediator _mediator;

        public PropertyController(RentHarbor.AuthService.Services.IAuthorizationService authorizationService, IMediator mediator)
        {
            _authorizationService = authorizationService;
            _mediator = mediator;
        }

        [HttpPost("getAllFollowed")]
        public async Task<ActionResult> GetAllFollowedProperties([FromBody] UpdateBasketRequest request)
        {
            string token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var userId = await _authorizationService.GetUserIdFromTokenAsync(token);

            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var command = new GetFollowedPropertiesQuery
            {
                UserId = userId,
            };

            var result = _mediator.Send(command);

            return Ok(new { Status = HttpStatusCode.OK });
        }
    }
}
