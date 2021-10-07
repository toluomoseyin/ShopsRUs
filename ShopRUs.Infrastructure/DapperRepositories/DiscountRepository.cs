using Dapper;
using Microsoft.Data.Sqlite;
using ShopRUs.Core.DapperRepositories;
using ShopRUs.Core.Models;
using ShopRUs.Infrastructure.Seeder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopRUs.Infrastructure.DapperRepositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly DatabaseConfig _databaseConfig;

        public DiscountRepository(DatabaseConfig databaseConfig)
        {
            _databaseConfig = databaseConfig ?? throw new ArgumentNullException(nameof(databaseConfig));
        }

        public async Task<bool> CreateDiscount(Discount discount)
        {
            using var connection = new SqliteConnection(_databaseConfig.Name);
            var affected = await connection.ExecuteAsync("INSERT INTO Discount (Name, Description, DiscountPercent, Created_at,Modified_at) VALUES (@Discount, @Description, @DiscountPercent,@Created_at,@Modified_at)",
                new { Name = discount.Name, Description = discount.Description, DiscountPercent = discount.DiscountPercent, Created_at = discount.Created_at, Modified_at = discount.Modified_at});
            if (affected == 0)
            {
                return false;
            }
            return true;
        }

        public async Task<Discount> GetDiscountByType(string type)
        {
            using var connection = new SqliteConnection(_databaseConfig.Name);
            var discount = await connection.QueryFirstOrDefaultAsync<Discount>("SELECT * FROM Discount WHERE Name=@Name", new { Name = type });
            return discount;
        }

        public async Task<IEnumerable<Customer>> GetDiscounts()
        {
            using var connection = new SqliteConnection(_databaseConfig.Name);
            var discount = await connection.QueryAsync<Customer>("SELECT * FROM Discount");
            return discount;
        }
    }
}
