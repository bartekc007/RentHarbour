using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Domain.Document.AddDocument;
using Ordering.Application.Domain.Document.GetDocumentsByOfferId;
using RentHarbor.AuthService.Services;
using System.Text.Json;
using System.Threading.Tasks;

namespace Ordering.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OfferDocumentsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public readonly IAuthorizationService _authorizationService;

        public OfferDocumentsController(IMediator mediator, IAuthorizationService authorizationService)
        {
            _mediator = mediator;
            _authorizationService = authorizationService;
        }

        [HttpPost("UploadDocument")]
        public async Task<IActionResult> UploadDocument([FromForm] AddDocumentCommand request)
        {
            // Authorization logic
            string token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var userIdJson = await _authorizationService.GetUserIdFromTokenAsync(token);
            JsonDocument doc = JsonDocument.Parse(userIdJson);
            JsonElement root = doc.RootElement;
            string userId = root.GetProperty("userId").GetString();

            // Create the command
            var command = new AddDocumentCommand
            {
                OfferId = request.OfferId,
                UserId = userId,
                File = request.File
            };

            // Handle the command
            var document = await _mediator.Send(command);

            return Ok(new { Data = document.DocumentId, document.FileName, document.UploadDate });
        }


        [HttpGet("{offerId}/documents")]
        public async Task<IActionResult> GetDocumentsByOfferId(int offerId)
        {
            string token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var userIdJson = await _authorizationService.GetUserIdFromTokenAsync(token);
            JsonDocument doc = JsonDocument.Parse(userIdJson);
            JsonElement root = doc.RootElement;
            string userId = root.GetProperty("userId").GetString();

            var query = new GetDocumentsByOfferIdQuery
            {
                OfferId = offerId,
                UserId = userId
            };

            var documents = await _mediator.Send(query);

            return Ok(documents);
        }
    }

}
