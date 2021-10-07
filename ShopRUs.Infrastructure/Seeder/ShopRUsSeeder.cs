using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using ShopRUs.Core.Models;
using ShopRUs.Infrastructure.Data;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace ShopRUs.Infrastructure.Seeder
{
    public class ShopRUsSeeder
    {

        public static async Task SeedDatabase(IApplicationBuilder app)
        {
            var dbContext = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<ShopRUsDbContext>();

            dbContext.Database.EnsureCreated();
            await SeedDiscount(dbContext);
            await SeedCustomer(dbContext);


        }
         
        private static async Task SeedCustomer(ShopRUsDbContext shopRUsDbContext)
        {
            if (!shopRUsDbContext.Customers.Any())
            {
                var fairPayUsersStream = await File.ReadAllTextAsync("../ShopRUs.Infrastructure/JsonFiles/Customer.json");
                var fairPayUsers = JsonSerializer.Deserialize<IEnumerable<Customer>>(fairPayUsersStream);
                await shopRUsDbContext.Customers.AddRangeAsync(fairPayUsers);
                await shopRUsDbContext.SaveChangesAsync(); 
            }

        }
        private static async Task SeedDiscount(ShopRUsDbContext shopRUsDbContext)
        {
            if (!shopRUsDbContext.Discounts.Any())
            {
                var discountStream = await File.ReadAllTextAsync("../ShopRUs.Infrastructure/JsonFiles/Discount.json");
                var discounts = JsonSerializer.Deserialize<IEnumerable<Discount>>(discountStream);
                await shopRUsDbContext.Discounts.AddRangeAsync(discounts);
                await shopRUsDbContext.SaveChangesAsync();
            }

        }
    }
}

