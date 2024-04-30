using MongoDB.Driver;
using RentHarbor.MongoDb.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentHarbor.MongoDb.Data
{
    public interface ICatalogContext
    {
        IMongoCollection<Property> Properties { get; }
        IMongoCollection<UserProperty> FollowedProperties { get; }
    }
}
