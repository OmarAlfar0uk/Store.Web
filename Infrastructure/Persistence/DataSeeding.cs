using DomainLayer.Contracts;
using DomainLayer.Models.IdentityModule;
using DomainLayer.Models.ProductModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Persistence.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Persistence
{
    public class DataSeeding(StoreDbContext _dbContext ,
        UserManager<ApplicationUser> _userManager , 
        RoleManager<IdentityRole> _roleManager , 
        StoreIdentityDbContext _identityDbContext) : IDataSeeding
    { 
        public async Task DataSeedAsync()
        {
            try
            {
                var PendingMigrations = await _dbContext.Database.GetPendingMigrationsAsync();
                if (PendingMigrations.Any())
                {
                  await  _dbContext.Database.MigrateAsync();
                }

                if (!_dbContext.ProductBrands.Any())
                {
                    var ProductBrandData =  File.OpenRead(@"..\Infrastructure\Persistence\Data\DataSeed\brands.json");
                    var ProductBrands =await JsonSerializer.DeserializeAsync<List<ProductBrand>>(ProductBrandData);
                    if (ProductBrands is not null && ProductBrands.Any())
                     await   _dbContext.ProductBrands.AddRangeAsync(ProductBrands);
                }

                if (!_dbContext.productTypes.Any())
                {
                    var ProductTypeData = File.OpenRead(@"..\Infrastructure\Persistence\Data\DataSeed\types.json");
                    var ProductTypes =await JsonSerializer.DeserializeAsync<List<ProductType>>(ProductTypeData);
                    if (ProductTypes is not null && ProductTypes.Any())
                     await   _dbContext.productTypes.AddRangeAsync(ProductTypes);
                }
                await _dbContext.SaveChangesAsync();

                if (!_dbContext.Products.Any())
                {
                    var ProductData = File.OpenRead(@"..\Infrastructure\Persistence\Data\DataSeed\products.json");
                    var Products =await JsonSerializer.DeserializeAsync<List<Product>>(ProductData);
                    if (Products is not null && Products.Any())
                     await   _dbContext.Products.AddRangeAsync(Products);
                }
              await  _dbContext.SaveChangesAsync();

            }

            catch(Exception ex)
            {

            }
        }

        public async Task IdentityDataSeedAsync()
        {
            try
            {
                if (!_roleManager.Roles.Any())
                {
                    await _roleManager.CreateAsync(new IdentityRole("Admin"));
                    await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
                }

                if (_userManager.Users.Any())
                {
                    var User01 = new ApplicationUser()
                    {
                        Email = "omarAlfarouk@gamil.com",
                        DisplayName = "Omar Alfarouk",
                        PhoneNumber = "01226801925",
                        UserName = "OmarAlfarouk"
                    };

                    var User02 = new ApplicationUser()
                    {
                        Email = "yaratharwat@gamil.com",
                        DisplayName = "Yara Tharwat",
                        PhoneNumber = "01226801925",
                        UserName = "yaratharwat"
                    };

                    await _userManager.CreateAsync(User01, "P@ss0rd");
                    await _userManager.CreateAsync(User02, "P@ss0rd");

                    await _userManager.AddToRoleAsync(User01, "Admin");
                    await _userManager.AddToRoleAsync(User02, "SuperAdmin");
                }

                await _identityDbContext.SaveChangesAsync();
            }
            catch (Exception ex) 
            { 
            }

        }
    }
}
