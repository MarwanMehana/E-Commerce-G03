using DomainLayer.Contracts;
using DomainLayer.Models.ProductModule;
using Microsoft.EntityFrameworkCore;
using Persistence.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistence
{
    public class DataSeeding(StoreDbContext _dbContext) : IDataSeeding
    {
        public async Task DataseedAsync()
        {
            try
            {
                if ((await _dbContext.Database.GetPendingMigrationsAsync()).Any())
                {
                    await _dbContext.Database.MigrateAsync();
                }

                if (!_dbContext.ProductBrands.Any())
                {
                    //var productBrandsData = await File.ReadAllTextAsync(@"..\InfraStructure\\Persistence\\Data\\DataSeed\\brands.json");
                    var productBrandsData = File.OpenRead(@"..\InfraStructure\\Persistence\\Data\\DataSeed\\brands.json");
                    var productBrands = await JsonSerializer.DeserializeAsync<List<ProductBrand>>(productBrandsData);
                    if (productBrands is not null && productBrands.Any())
                    {
                        await _dbContext.ProductBrands.AddRangeAsync(productBrands);
                    }
                }
                if (!_dbContext.ProductTypes.Any())
                {
                    var productTypesData = File.OpenRead(@"..\InfraStructure\\Persistence\\Data\\DataSeed\\typess.json");
                    var productTypes = await JsonSerializer.DeserializeAsync<List<ProductType>>(productTypesData);
                    if (productTypes is not null && productTypes.Any())
                    {
                        await _dbContext.ProductTypes.AddRangeAsync(productTypes);
                    }
                }
                if (!_dbContext.Products.Any())
                {
                    var productsData = File.OpenRead(@"..\InfraStructure\\Persistence\\Data\\DataSeed\\Products.json");
                    var products = await JsonSerializer.DeserializeAsync<List<ProductType>>(productsData);
                    if (products is not null && products.Any())
                    {
                        await _dbContext.ProductTypes.AddRangeAsync(products);
                    }
                }

                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                // To do
            }
        }
    }
}
