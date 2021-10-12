using ShopRUs.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopRUs.Core.Repositories
{
    public interface ICustomerTypeRepository
    {
        Task<IEnumerable<CustomerType>> GetAll();
    }
}
