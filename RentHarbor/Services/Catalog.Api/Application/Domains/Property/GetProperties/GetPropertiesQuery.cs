using MediatR;

namespace Catalog.Application.Domains.Property.GetProperties
{
    public class GetPropertiesQuery : IRequest<GetPropertiesResult>
    {
        public double? PriceMax { get; set; }
        public double? PriceMin { get; set; }
        public int? BedroomsMax { get; set; }
        public int? BedroomsMin { get; set; }
        public int? BathroomsMax { get; set; }
        public int? BathroomsMin { get; set; }
        public int? AreaSquareMetersMax { get; set; }
        public int? AreaSquareMetersMin { get; set; }
        public string? City { get; set; }
    }
}
