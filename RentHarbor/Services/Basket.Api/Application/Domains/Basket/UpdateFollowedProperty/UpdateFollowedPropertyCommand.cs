using MediatR;

namespace Basket.Application.Domains.Basket.UpdateFollowedProperty
{
    public class UpdateFollowedPropertyCommand : IRequest<UpdateFollowedPropertyResult>
    {
        public string Id { get; set; }

        public string UserId { get; set; }

        public string PropertyId { get; set; }

        public UserPropertyAction Action { get; set; }
    }

    public enum UserPropertyAction
    {
        Add,
        Remove
    }
}
