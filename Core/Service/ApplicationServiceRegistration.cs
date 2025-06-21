using Microsoft.Extensions.DependencyInjection;
using ServiceAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection Services)
        {
            Services.AddAutoMapper(typeof(Service.AssemblyReference).Assembly);
            Services.AddScoped<IServiceManger, ServiceMangerWithFactoryDelegate>();

            Services.AddScoped<IProductService, ProductService>();
            Services.AddScoped<Func<IProductService>>(provider =>
            () => provider.GetRequiredService<IProductService>());

            Services.AddScoped<IOrederService, OrderService>();
            Services.AddScoped<Func<IOrederService>>(provider =>
            () => provider.GetRequiredService<IOrederService>());

            Services.AddScoped<IAuthenticationService, AuthenticationService>();
            Services.AddScoped<Func<IAuthenticationService>>(provider =>
            () => provider.GetRequiredService<IAuthenticationService>());

            Services.AddScoped<IBasketService, BasketService>();
            Services.AddScoped<Func<IBasketService>>(provider =>
            () => provider.GetRequiredService<IBasketService>());

            Services.AddScoped<ICacheService, CacheService>();

            Services.AddScoped<IPaymentService, PaymentService>();
            Services.AddScoped<Func<IPaymentService>>(provider =>
            () => provider.GetRequiredService<IPaymentService>());

            return Services;
        } 
    }
}
