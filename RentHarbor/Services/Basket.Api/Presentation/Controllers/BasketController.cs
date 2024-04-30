using Basket.Api.Requests;
using Basket.Application.Domains.Basket.UpdateBasket;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace Basket.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BasketController : ControllerBase
    {
        public readonly RentHarbor.AuthService.Services.IAuthorizationService _authorizationService;
        public readonly IMediator _mediator;

        public BasketController(RentHarbor.AuthService.Services.IAuthorizationService authorizationService, IMediator mediator)
        {
            _authorizationService = authorizationService;
            _mediator = mediator;
        }

        [HttpPost("update")]
        public async Task<ActionResult> UpdateBasket([FromBody] UpdateBasketRequest request)
        {
            // Pobierz obiekt kontekstu HTTP z akcesorem
            string token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var userId = await _authorizationService.GetUserIdFromTokenAsync(token);

            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var command = new UpdateBasketCommand()
            {
                UserId = userId,
                PropertyId = request.PropertyId,
                Action = request.Action
            };

            var result = _mediator.Send(command);

            return Ok(new { Status = HttpStatusCode.OK });
        }

    }
}
