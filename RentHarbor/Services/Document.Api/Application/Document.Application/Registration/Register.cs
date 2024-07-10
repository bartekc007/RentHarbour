using Document.Application.Domain.Document.AddDocument;
using Document.Application.Domain.Document.Common.Dto;
using Document.Application.Domain.Document.GetDocumentsByOfferId;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace Document.Application.Registration
{
    public static class Register
    {
        public static void RegisterApplicationLayer(this IServiceCollection services)
        {
            services.AddTransient<IRequestHandler<GetDocumentsByOfferIdQuery, List<OfferDocumentDto>>, GetDocumentsByOfferIdHandler>();
            services.AddTransient<IRequestHandler<AddDocumentCommand, OfferDocumentDto>, AddDocumentCommandHandler>();
        }
    }
}
