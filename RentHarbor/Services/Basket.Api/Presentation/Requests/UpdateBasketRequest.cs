using Basket.Application.Domains.Basket.UpdateBasket;

namespace Basket.Api.Requests
{
    public class UpdateBasketRequest
    {
        public string PropertyId { get; set; }
        public BasketAction Action { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string AddressStreet { get; set; }
        public string AddressCity { get; set; }
        public string AddressState { get; set; }
        public string AddressPostalCode { get; set; }
        public string AddressCountry { get; set; }
        public double Price { get; set; }
        public int Bedrooms { get; set; }
        public int Bathrooms { get; set; }
        public int AreaSquareMeters { get; set; }
        public bool IsAvailable { get; set; }
    }
}
