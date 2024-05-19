using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Application.Domains.Basket.GetFollowedProperties.Dto
{
    public class PropertyDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public AddressDto Address { get; set; }
        public decimal Price { get; set; }
        public int Bedrooms { get; set; }
        public int Bathrooms { get; set; }
        public int AreaSquareMeters { get; set; }
        public bool IsAvailable { get; set; }
        public List<BsonDocument> Photos { get; set; }
        public PropertyDto()
        {
            Photos = new List<BsonDocument>();
        }
    }
}
