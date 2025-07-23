using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using TelephoneBook.Infrastructure.Contrates;
using TelephoneBook.Infrastructure.Interfaces;

namespace TelephoneBook.Infrastructure
{
    public static class InfraDependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            
            services.AddSingleton<IMongoClient>(sp =>
                new MongoClient("mongodb://localhost:27017"));

            services.AddScoped(sp =>
            {
                var client = sp.GetRequiredService<IMongoClient>();
                return client.GetDatabase("mydb");
            });

            services.AddScoped(typeof(IRepository<>), typeof(MongoRepository<>));

            return services;
        }
    }
}
