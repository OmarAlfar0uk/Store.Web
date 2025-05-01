using DomainLayer.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Data;
using Persistence.Repositorice;
using Service;
using Service.MappingProfile;
using ServiceAbstraction;
using Shared.ErrorModels;
using Store.Web.CustomeMiddleWares;
using Store.Web.Extensions;
using Store.Web.Factories;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace Store.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            # region services to the container.
            builder.Services.AddControllers();
         

            builder.Services.AddSwaggerServices();

            builder.Services.AddInfrastructureService(builder.Configuration);
            builder.Services.AddApplicationServices();
            builder.Services.AddWebApplicationServices();
            builder.Services.AddJWTService(builder.Configuration);
            #endregion


            var app = builder.Build();

            await app.SeedDataBaseAsync();

            app.UseCustomExceptionMiddelWare();

            #region Configure the HTTP request pipeline.

            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerMiddleWear();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization(); 


            app.MapControllers();
            #endregion


            app.Run();
        }
    }
}
