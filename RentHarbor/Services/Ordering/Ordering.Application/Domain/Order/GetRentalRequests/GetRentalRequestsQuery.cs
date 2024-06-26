﻿using MediatR;
using Ordering.Persistance.Entities;

namespace Ordering.Application.Domain.Order.GetRentalRequests
{
    public class GetRentalRequestsQuery : IRequest<GetRentalRequestsQueryResult>
    {
        public string OwnerId { get; set; }  // User ID of the property owner
    }
}
