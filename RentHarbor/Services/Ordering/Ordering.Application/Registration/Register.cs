using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Application.Domain.Order.AcceptRentalRequest;
using Ordering.Application.Domain.Order.CreateRentalRequest;
using Ordering.Application.Domain.Order.GetRentalOfferById;
using Ordering.Application.Domain.Order.GetRentalRequestByUserId;
using Ordering.Application.Domain.Order.GetRentalRequests;
using Ordering.Application.Domain.Payment.GetPayments;
using Ordering.Application.Domain.Payment.GetPayments.Dto;

namespace Ordering.Application.Registration
{
    public static class Register
    {
        public static void RegisterApplicationLayer(this IServiceCollection services)
        {
            services.AddTransient<IRequestHandler<CreateRentalRequestCommand, CreateRentalRequestCommandResult>, CreateRentalRequestCommandHandler>();
            services.AddTransient<IRequestHandler<GetRentalRequestsQuery, GetRentalRequestsQueryResult>, GetRentalRequestsQueryHandler>();
            services.AddTransient<IRequestHandler<GetRentalRequestByUserIdQuery, GetRentalRequestByUserIdQueryResult>, GetRentalRequestByUserIdQueryHandler>();
            services.AddTransient<IRequestHandler<GetRentalRequestQuery, GetRentalRequestQueryResult>, GetRentalRequestQueryHandler>();
            services.AddTransient<IRequestHandler<AcceptRentalRequestCommand, bool>, AcceptRentalRequestCommandHandler>();
            services.AddTransient<IRequestHandler<GetPaymentsQuery, IEnumerable<PaymentDto>>, GetPaymentsQueryHandler>();

            services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<AcceptRentalRequestCommandValidator>());
            services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateRentalRequestCommandValidator>());
            services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<GetRentalRequestQueryValidator>());
            services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<GetRentalRequestByUserIdQueryValidator>());
            services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<GetRentalRequestsQueryValidator>());
            services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<GetPaymentsQueryValidator>());
        }
    }
}
