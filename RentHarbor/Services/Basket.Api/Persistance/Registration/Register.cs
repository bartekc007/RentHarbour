using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RentHarbor.MongoDb.Registration;

namespace Basket.Persistance.Registration
{
    public static class Register
    {
        public static void RegisterPersistanceLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.RegisterMongoDb(configuration);
        }
    }
}
