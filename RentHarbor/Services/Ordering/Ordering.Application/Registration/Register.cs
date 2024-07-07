using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Application.Domain.Document.AddDocument;
using Ordering.Application.Domain.Document.Common.Dto;
using Ordering.Application.Domain.Document.GetDocumentsByOfferId;
using Ordering.Application.Domain.Order.AcceptRentalRequest;
using Ordering.Application.Domain.Order.CreateRentalRequest;
using Ordering.Application.Domain.Order.GetRentalOfferById;
using Ordering.Application.Domain.Order.GetRentalRequests;

namespace Ordering.Application.Registration
{
    public static class Register
    {
        public static void RegisterApplicationLayer(this IServiceCollection services)
        {
            //services.AddTransient<IRequestHandler<RentPropertyCommand, RentPropertyResult>, RentPropertyCommandHandler>();
            services.AddTransient<IRequestHandler<CreateRentalRequestCommand, CreateRentalRequestCommandResult>, CreateRentalRequestCommandHandler>();
            services.AddTransient<IRequestHandler<GetRentalRequestsQuery, GetRentalRequestsQueryResult>, GetRentalRequestsQueryHandler>();
            services.AddTransient<IRequestHandler<GetRentalRequestQuery, GetRentalRequestQueryResult>, GetRentalRequestQueryHandler>();
            services.AddTransient<IRequestHandler<GetDocumentsByOfferIdQuery, List<OfferDocumentDto>>, GetDocumentsByOfferIdHandler>();
            services.AddTransient<IRequestHandler<AddDocumentCommand, OfferDocumentDto>, AddDocumentCommandHandler>();
            services.AddTransient<IRequestHandler<AcceptRentalRequestCommand, bool>, AcceptRentalRequestCommandHandler>();
        }
    }
}
