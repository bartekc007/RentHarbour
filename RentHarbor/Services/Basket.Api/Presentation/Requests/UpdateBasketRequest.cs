using Basket.Application.Domains.Basket.UpdateBasket;

namespace Basket.Api.Requests
{
    public class UpdateBasketRequest
    {
        public string PropertyId { get; set; }
        public BasketAction Action { get; set; }
    }
}
