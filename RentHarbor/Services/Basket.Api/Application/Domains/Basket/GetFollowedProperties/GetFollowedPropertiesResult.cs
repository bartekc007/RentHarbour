namespace Basket.Application.Domains.Basket.GetFollowedProperties
{
    public class GetFollowedPropertiesResult
    {
        public List<Dto.PropertyDto> Data;
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
    }
}
