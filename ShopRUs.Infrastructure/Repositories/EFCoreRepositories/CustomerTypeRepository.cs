using Microsoft.EntityFrameworkCore;
using ShopRUs.Core.Models;
using ShopRUs.Core.Repositories;
using ShopRUs.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopRUs.Infrastructure.Repositories.EFCoreRepositories
{
    public class CustomerTypeRepository:ICustomerTypeRepository
    {
        private readonly ShopRUsDbContext _shopRUsDbContext;

        public CustomerTypeRepository(ShopRUsDbContext shopRUsDbContext)
        {
            _shopRUsDbContext = shopRUsDbContext ?? throw new ArgumentNullException(nameof(shopRUsDbContext));
        }

        public async Task<IEnumerable<CustomerType>> GetAll()
        {
            return await _shopRUsDbContext.CustomerTypes.ToListAsync();
        }
    }
}
