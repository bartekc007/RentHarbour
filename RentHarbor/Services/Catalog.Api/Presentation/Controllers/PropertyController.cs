using Catalog.Application.Application.Domains.Property.GetProperties;
using Catalog.Application.Application.Domains.Property.GetPropertyById;
using Catalog.Application.Application.Domains.Property.GetRentedProperites;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RentHarbor.AuthService.Services;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace Catalog.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PropertyController : ControllerBase
    {
        private readonly IMediator _mediator;
        public readonly IAuthorizationService _authorizationService;

        public PropertyController(IMediator mediator, IAuthorizationService authorizationService)
        {
            _mediator = mediator;
            _authorizationService = authorizationService;
        }

        [HttpGet("all")]
        public async Task<ActionResult<GetPropertiesResult>> GetProperties([FromQuery] GetPropertiesQuery query)
        {
            var result = await _mediator.Send(query);

            if (result.Data == null || result.Data.Count == 0)
            {
                return Ok(new { Status = HttpStatusCode.NoContent, Data = result.Data });
            }

            return Ok(new { Status = HttpStatusCode.OK, Data = result.Data });
        }

        [HttpGet("rented")]
        public async Task<ActionResult<GetRentedPropertiesQueryResult>> GetRentedProperties()
        {
            // Authorization logic
            string token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var userIdJson = await _authorizationService.GetUserIdFromTokenAsync(token);
            JsonDocument doc = JsonDocument.Parse(userIdJson);
            JsonElement root = doc.RootElement;
            string userId = root.GetProperty("userId").GetString();

            var query = new GetRentedPropertiesQuery
            {
                UserId = userId
            };

            var result = await _mediator.Send(query);

            if (result.Data == null || result.Data.Count == 0)
            {
                return Ok(new { Status = HttpStatusCode.NoContent, Data = result.Data });
            }

            return Ok(new { Status = HttpStatusCode.OK, Data = result.Data });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPropertyById(string id)
        {
            try
            {
                var query = new GetPropertyByIdQuery(id);
                var result = await _mediator.Send(query);

                if (result == null)
                {
                    return NotFound();
                }

                return Ok(new { Status = HttpStatusCode.OK, Data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
