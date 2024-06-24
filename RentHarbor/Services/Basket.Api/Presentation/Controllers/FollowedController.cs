using Basket.Api.Requests;
using Basket.Application.Domains.Basket.GetFollowedProperties;
using Basket.Application.Domains.Basket.UpdateFollowedProperty;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace Basket.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FollowedController : ControllerBase
    {
        public readonly RentHarbor.AuthService.Services.IAuthorizationService _authorizationService;
        public readonly IMediator _mediator;

        public FollowedController(RentHarbor.AuthService.Services.IAuthorizationService authorizationService, IMediator mediator)
        {
            _authorizationService = authorizationService;
            _mediator = mediator;
        }

        [HttpPost("update")]
        public async Task<ActionResult> UpdateFollowedProperties([FromBody] UpdateFollowedPropertiesRequest request)
        {
            // Pobierz obiekt kontekstu HTTP z akcesorem
            string token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var userId = await _authorizationService.GetUserIdFromTokenAsync(token);

            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var command = new UpdateFollowedPropertyCommand()
            {
                UserId = userId,
                PropertyId = request.PropertyId,
                Action = request.Action,
                Id = request.Id
            };

            var result = await _mediator.Send(command);

            return Ok(new { Status = HttpStatusCode.OK, Data = result.Success });
        }

        [HttpPost("getAll")]
        public async Task<ActionResult> GetAllFollowedProperties([FromBody] GetFollowedPropertiesQuery request)
        {
            // Pobierz obiekt kontekstu HTTP z akcesorem
            string token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var userId = await _authorizationService.GetUserIdFromTokenAsync(token);

            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

/*            var command = new UpdateFollowedPropertyCommand()
            {
                UserId = userId,
                PropertyId = request.PropertyId,
                Action = request.Action,
                Id = request.Id
            };

            var result = _mediator.Send(command);*/

            return Ok(new { Status = HttpStatusCode.OK });
        }
    }
}
