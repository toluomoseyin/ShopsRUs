using Microsoft.EntityFrameworkCore;
using ShopRUs.Core.Models;
using ShopRUs.Core.Repositories;
using ShopRUs.Infrastructure.Data;
using ShopRUs.Infrastructure.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopRUs.Infrastructure.Repositories
{
    class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(ShopRUsDbContext shopRUsDbContext) : base(shopRUsDbContext) { }
        public async Task<IEnumerable<Customer>> GetEmployeeByLastName(string lastname)
        {
            return await _shopRUsDbContext.Customers.Where(m => m.LastName == lastname).ToListAsync();
        }

        public async Task<Customer> AddCustomer(Customer customer)
        {
            return  await AddAsync(customer);
        }

        public async Task<Customer>  GetCustomerById(int id)
        {
            return await  GetByIdAsync(id);
        }

        public async Task<IReadOnlyList<Customer>> GetAllCustomer()
        {
            return await GetAllAsync();
        }

        public async Task DeleteCustomer(int id)
        {
            var customer = await GetCustomerById(id);
           await  DeleteAsync(customer);
        }


    }
}
