using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Application.Domain.Order.CreateRentalRequest;
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

        }
    }
}
