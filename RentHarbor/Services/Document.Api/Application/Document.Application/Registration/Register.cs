using Document.Application.Domain.Document.AddDocument;
using Document.Application.Domain.Document.Common.Dto;
using Document.Application.Domain.Document.GetDocumentById;
using Document.Application.Domain.Document.GetDocumentsByOfferId;
using FluentValidation.AspNetCore;
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
            services.AddTransient<IRequestHandler<GetDocumentByIdQuery, OfferDocumentDto>, GetDocumentByIdQueryHandler>();

            services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<AddDocumentCommandValidator>());
            services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<GetDocumentByIdQueryValidator>());
            services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<GetDocumentsByOfferIdQueryValidator>());
        }
    }
}
