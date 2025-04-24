
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
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddScoped<IDataSeeding, DataSeeding>();

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddAutoMapper(typeof(Service.AssemblyReference).Assembly);
            builder.Services.AddScoped<IServiceManger , ServiceManger>();
            builder.Services.Configure<ApiBehaviorOptions>((Options) =>
            {
                Options.InvalidModelStateResponseFactory = ApiResponseFactories.GenerateApiValidationErrorsRespons;
            });
            #endregion


            var app = builder.Build();

           using var Scoope = app.Services.CreateScope();
           var ObjectOfDataSeeding =  Scoope.ServiceProvider.GetRequiredService<IDataSeeding>();
          await  ObjectOfDataSeeding.DataSeedAsync();

            app.UseMiddleware<CustomeExceptionHandlerMiddleWares>();
            #region Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();



            app.MapControllers();
            #endregion


            app.Run();
        }
    }
}
