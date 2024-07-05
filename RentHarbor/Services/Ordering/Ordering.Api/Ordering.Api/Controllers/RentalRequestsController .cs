using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Ordering.Application.Domain.Order.CreateRentalRequest;
using Ordering.Application.Domain.Order.GetRentalRequests;
using Ordering.Api.Request;
using System.Net;
using System.Text.Json;

namespace Ordering.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RentalRequestsController : ControllerBase
    {
        public readonly RentHarbor.AuthService.Services.IAuthorizationService _authorizationService;
        private readonly IMediator _mediator;

        public RentalRequestsController(IMediator mediator, RentHarbor.AuthService.Services.IAuthorizationService authorizationService)
        {
            _mediator = mediator;
            _authorizationService = authorizationService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateRentalRequest([FromBody] RentalRequestRequest request)
        {
            string token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var userIdJson = await _authorizationService.GetUserIdFromTokenAsync(token);
            JsonDocument doc = JsonDocument.Parse(userIdJson);

            JsonElement root = doc.RootElement;
            string userId = root.GetProperty("userId").GetString();

            var command = new CreateRentalRequestCommand
            {
                PropertyId = request.PropertyId,
                UserId = userId,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                Pets = request.Pets,
                NumberOfPeople = request.NumberOfPeople,
                MessageToOwner = request.MessageToOwner
            };

            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var result = await _mediator.Send(command);
            return Ok(new { Data = result, Status = HttpStatusCode.OK });
        }

        [HttpGet]
        public async Task<IActionResult> GetRentalRequests()
        {
            string token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var userIdJson = await _authorizationService.GetUserIdFromTokenAsync(token);
            JsonDocument doc = JsonDocument.Parse(userIdJson);

            JsonElement root = doc.RootElement;
            string userId = root.GetProperty("userId").GetString();

            var query = new GetRentalRequestsQuery { OwnerId = userId };
            var rentalRequests = await _mediator.Send(query);
            return Ok(new { Data = rentalRequests.Data, Status = HttpStatusCode.OK });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRentalRequest([FromQuery] string offerId)
        {
            string token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var userIdJson = await _authorizationService.GetUserIdFromTokenAsync(token);
            JsonDocument doc = JsonDocument.Parse(userIdJson);

            JsonElement root = doc.RootElement;
            string userId = root.GetProperty("userId").GetString();

            var query = new GetRentalRequestsQuery { OwnerId = userId };
            var rentalRequest = await _mediator.Send(query);
            if (rentalRequest.Data == null)
                return Ok(new { message = "Rental request not found", Status = HttpStatusCode.NotFound });
            
            return Ok(new { Data = rentalRequest.Data, Status = HttpStatusCode.OK });
        }
    }

}
