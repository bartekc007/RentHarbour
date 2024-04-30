using Catalog.Application.Application.Domains.Property.GetProperties;
using Catalog.Application.Application.Domains.Property.GetPropertyById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Catalog.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PropertyController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PropertyController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("all")]
        public async Task<ActionResult<GetPropertiesResult>> GetProperties()
        {
            var query = new GetPropertiesQuery();
            var result = await _mediator.Send(query);

            if (result.Data == null || result.Data.Count == 0)
            {
                return NoContent();
            }

            return Ok(new {Status = HttpStatusCode.OK, result.Data });
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

                return Ok(new { Status = HttpStatusCode.OK, result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
