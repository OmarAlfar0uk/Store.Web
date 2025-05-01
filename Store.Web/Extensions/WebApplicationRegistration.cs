using DomainLayer.Contracts;
using Store.Web.CustomeMiddleWares;
using System.Diagnostics;

namespace Store.Web.Extensions
{
    public static class WebApplicationRegistration
    {
        public static async Task SeedDataBaseAsync(this WebApplication app)
        {

            using var Scoope = app.Services.CreateScope();
            var ObjectOfDataSeeding = Scoope.ServiceProvider.GetRequiredService<IDataSeeding>();
            await ObjectOfDataSeeding.DataSeedAsync();

        }

        public static IApplicationBuilder UseCustomExceptionMiddelWare(this IApplicationBuilder app) 
        {
            app.UseMiddleware<CustomeExceptionHandlerMiddleWares>();

            return app;
        }

        public static IApplicationBuilder UseSwaggerMiddleWear(this IApplicationBuilder app) 
        {

            app.UseSwagger();
            app.UseSwaggerUI();
            return app;
        }
    }
}
