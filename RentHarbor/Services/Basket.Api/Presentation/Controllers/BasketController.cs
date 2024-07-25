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
            string token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var userId = await _authorizationService.GetUserIdFromTokenAsync(token);

            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var command = new UpdateBasketCommand()
            {
                UserId = userId,
                PropertyId = request.PropertyId,
                Action = request.Action,
                Name = request.Name,
                Description = request.Description,
                AddressStreet = request.AddressStreet,
                AddressCity = request.AddressCity,
                AddressState = request.AddressState,
                AddressPostalCode = request.AddressPostalCode,
                AddressCountry = request.AddressCountry,
                Price = request.Price,
                Bedrooms = request.Bedrooms,
                Bathrooms = request.Bathrooms,
                AreaSquareMeters = request.AreaSquareMeters,
                IsAvailable = request.IsAvailable
            };

            var result = _mediator.Send(command);

            return Ok(new { Status = HttpStatusCode.OK });
        }
    }
}
