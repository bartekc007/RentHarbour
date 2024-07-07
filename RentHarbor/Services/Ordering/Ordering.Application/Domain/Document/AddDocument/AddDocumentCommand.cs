﻿using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Domain.Document.Common.Dto;

namespace Ordering.Application.Domain.Document.AddDocument
{
    public class AddDocumentCommand : IRequest<OfferDocumentDto>
    {
        [FromForm]
        public int OfferId { get; set; }
        public string? UserId { get; set; }
        [FromForm]
        public IFormFile File { get; set; }
    }
}
