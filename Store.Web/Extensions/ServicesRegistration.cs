using Microsoft.AspNetCore.Mvc;
using Store.Web.Factories;

namespace Store.Web.Extensions
{
    public static class ServicesRegistration
    {
        public static IServiceCollection AddSwaggerServices(this IServiceCollection Services )
        {
            Services.AddEndpointsApiExplorer();
            Services.AddSwaggerGen();
            return Services;
        }

        public static IServiceCollection AddWebApplicationServices(this IServiceCollection Services) 
        {
            Services.Configure<ApiBehaviorOptions>((Options) =>
            {
                Options.InvalidModelStateResponseFactory = ApiResponseFactories.GenerateApiValidationErrorsRespons;
            });
            return Services;
        }
    }
}
