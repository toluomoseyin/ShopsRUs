using ShopRUs.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopRUs.Core.DapperRepositories
{
   public interface IDiscountRepository
    {
        Task<IEnumerable<Customer>> GetDiscounts();
        Task<Discount> GetDiscountByType(string type);
        Task<bool> CreateDiscount(Discount discount);
    }
}
