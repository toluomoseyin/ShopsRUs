using Microsoft.EntityFrameworkCore;
using ShopRUs.Core.Models;

namespace ShopRUs.Infrastructure.Data
{
    public class ShopRUsDbContext:DbContext
    {
        public ShopRUsDbContext(DbContextOptions<ShopRUsDbContext> options):base(options)
        {

        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Discount> Discounts { get; set; }
    }
}
