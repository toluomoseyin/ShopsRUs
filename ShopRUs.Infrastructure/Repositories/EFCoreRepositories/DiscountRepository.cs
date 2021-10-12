
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
    public class DiscountRepository : IDiscountRepository
    {
        private readonly ShopRUsDbContext _shopRUsDbContext;

        public DiscountRepository(ShopRUsDbContext shopRUsDbContext)
        {
            _shopRUsDbContext = shopRUsDbContext ?? throw new ArgumentNullException(nameof(shopRUsDbContext));
        }

        public async Task<Discount> AddAsync(Discount discount)
        {
            var createdDiscount = await _shopRUsDbContext.AddAsync(discount);
            await _shopRUsDbContext.SaveChangesAsync();
            return createdDiscount.Entity;
        }

        public Task DeleteAsync(Discount discount)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Discount>> GetAllAsync()
        {
            return await _shopRUsDbContext.Discounts.ToListAsync();
        }

        public Task<Discount> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

       

        public async Task<Discount> GetDiscountByType(string type)
        {
            return  await  _shopRUsDbContext.Discounts.FirstOrDefaultAsync(x => x.DiscountType.ToLower().Contains( type.ToLower()));
        }

        public Task UpdateAsync(Discount entity)
        {
            throw new NotImplementedException();
        }
    }
}
