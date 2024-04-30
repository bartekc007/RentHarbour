using MediatR;

namespace Basket.Application.Domains.Basket.UpdateBasket
{
    public class UpdateBasketCommand : IRequest<UpdateBasketResult>
    {
        public string UserId { get; set; }
        public string PropertyId { get; set; }
        public BasketAction Action { get; set; }
    }

    public enum BasketAction
    {
        Add,
        Remove
    }
}
