using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Domain.Payment.GetPayments;
using RentHarbor.AuthService.Services;
using System.Text.Json;
using System.Threading.Tasks;

namespace Ordering.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IMediator _mediator;
        public readonly IAuthorizationService _authorizationService;

        public PaymentController(IMediator mediator, IAuthorizationService authorizationService)
        {
            _mediator = mediator;
            _authorizationService = authorizationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPayments([FromQuery] string propertyId)
        {
            string token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var userIdJson = await _authorizationService.GetUserIdFromTokenAsync(token);
            JsonDocument doc = JsonDocument.Parse(userIdJson);
            JsonElement root = doc.RootElement;
            string userId = root.GetProperty("userId").GetString();

            var query = new GetPaymentsQuery
            {
                UserId = userId,
                PropertyId = propertyId
            };

            var result = await _mediator.Send(query);
            return Ok(result);
        }

/*        [HttpPost]
        public async Task<IActionResult> MakePayment([FromBody] CreatePaymentCommand command)
        {
            string token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var userIdJson = await _authorizationService.GetUserIdFromTokenAsync(token);
            JsonDocument doc = JsonDocument.Parse(userIdJson);
            JsonElement root = doc.RootElement;
            string userId = root.GetProperty("userId").GetString();

            var paymentId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetPayments), new { id = paymentId }, null);
        }*/
    }
}
