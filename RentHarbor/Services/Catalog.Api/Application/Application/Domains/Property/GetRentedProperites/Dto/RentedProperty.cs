using Catalog.Application.Extensions;
using System.Text.Json.Serialization;

namespace Catalog.Application.Application.Domains.Property.GetRentedProperites.Dto
{
    public class RentedProperty
    {
        public string PropertyId { get; set; }
        public string PropertyName { get; set; }
        public string PropertyStreet { get; set; }
        [JsonConverter(typeof(DateOnlyJsonConverter))]
        public DateOnly StartDate { get; set; }
        [JsonConverter(typeof(DateOnlyJsonConverter))]
        public DateOnly EndDate { get; set; }
    }
}
