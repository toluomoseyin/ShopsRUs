using ShopRUs.Core.Models;
using ShopRUs.Infrastructure.Seeder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopRUs.Infrastructure.Repositories.DapperRepositories
{
    public class CustomerDiscountRepository
    {
        private readonly DatabaseConfig _databaseConfig;

        public CustomerDiscountRepository(DatabaseConfig databaseConfig)
        {
            _databaseConfig = databaseConfig ?? throw new ArgumentNullException(nameof(databaseConfig));
        }

       
    }
}
