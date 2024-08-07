﻿using Document.Application.Domain.Document.AddDocument;
using Document.Application.Domain.Document.GetDocumentById;
using Document.Application.Domain.Document.GetDocumentsByOfferId;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RentHarbor.AuthService.Services;
using System.Net;
using System.Net.Mime;
using System.Text.Json;
using System.Threading.Tasks;

namespace Document.Api.Controllers
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
            string token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var userIdJson = await _authorizationService.GetUserIdFromTokenAsync(token);
            JsonDocument doc = JsonDocument.Parse(userIdJson);
            JsonElement root = doc.RootElement;
            string userId = root.GetProperty("userId").GetString();

            var command = new AddDocumentCommand
            {
                OfferId = request.OfferId,
                UserId = userId,
                File = request.File
            };

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

            return Ok(new { Data = documents, Status = HttpStatusCode.OK });
        }

        [HttpGet("DownloadDocument/{documentId}")]
        public async Task<IActionResult> DownloadDocument(string documentId)
        {
            string token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var userIdJson = await _authorizationService.GetUserIdFromTokenAsync(token);
            JsonDocument doc = JsonDocument.Parse(userIdJson);
            JsonElement root = doc.RootElement;
            string userId = root.GetProperty("userId").GetString();

            var query = new GetDocumentByIdQuery
            {
                DocumentId = documentId
            };

            var document = await _mediator.Send(query);

            if (document == null)
            {
                return NotFound();
            }

            var contentDisposition = new ContentDisposition
            {
                FileName = document.FileName,
                Inline = false,
            };
            Response.Headers.Add("Content-Disposition", contentDisposition.ToString());

            return File(document.Content, "application/pdf");
        }
    }

}
