using Basket.Application.Domains.Basket.UpdateFollowedProperty;

namespace Basket.Api.Requests
{
    public class UpdateFollowedPropertiesRequest
    {
        public string Id { get; set; }
        public string PropertyId { get; set; }
        public UserPropertyAction Action { get; set; }
    }
}
