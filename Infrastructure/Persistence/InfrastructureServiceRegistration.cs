

using Microsoft.Extensions.Configuration;
using StackExchange.Redis;

namespace Persistence
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureService(this IServiceCollection Services, IConfiguration Configuration)
        {
            Services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });
            Services.AddScoped<IDataSeeding, DataSeeding>();
            Services.AddScoped<IUnitOfWork, UnitOfWork>();
            Services.AddScoped<IBasketRepository, BasketRepository>();
            var configurationOptions = new ConfigurationOptions
            {
                EndPoints = { "localhost" },
                 AbortOnConnectFail = false
            };
            Services.AddSingleton<IConnectionMultiplexer>( (_) =>
            {
                return  ConnectionMultiplexer.Connect(configurationOptions /*Configuration.GetConnectionString("RedisConnectionString")*/);
            });


            return Services;
        }
    }
}
