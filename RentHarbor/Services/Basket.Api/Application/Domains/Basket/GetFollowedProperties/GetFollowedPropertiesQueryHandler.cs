using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Application.Domains.Basket.GetFollowedProperties
{
    public class GetFollowedPropertiesQueryHandler : IRequestHandler<GetFollowedPropertiesQuery, GetFollowedPropertiesResult>
    {
        public Task<GetFollowedPropertiesResult> Handle(GetFollowedPropertiesQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
