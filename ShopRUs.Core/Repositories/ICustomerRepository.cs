using ShopRUs.Core.Models;
using ShopRUs.Core.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopRUs.Core.Repositories
{
    public interface ICustomerRepository:IRepository<Customer>
    {
        Task<IEnumerable<Customer>> GetCustomerByName(string name);
    }
}
