using Microsoft.EntityFrameworkCore;
using ShopRUs.Application.DTOs;
using ShopRUs.Core.Models;
using ShopRUs.Core.Repositories;
using ShopRUs.Infrastructure.Data;
using System;
//using ShopRUs.Infrastructure.Repositories.Base;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopRUs.Infrastructure.Repositories.EFCoreRepositories
{
    public class CustomerRepository :  ICustomerRepository
    {

        private readonly ShopRUsDbContext _shopRUsDbContext;

        public CustomerRepository(ShopRUsDbContext shopRUsDbContext)
        {
            _shopRUsDbContext = shopRUsDbContext ?? throw new ArgumentNullException(nameof(shopRUsDbContext));
        }

       

        public async Task<Customer> GetCustomerByName(string name)
        {
            return await _shopRUsDbContext.Customers.FirstOrDefaultAsync(x => x.FirstName.ToLower().Contains( name.ToLower()) || x.LastName.ToLower().Contains(name.ToLower()));
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await _shopRUsDbContext.Customers.ToListAsync();
        }

        public async Task<Customer> GetByIdAsync(int id)
        {
            return await _shopRUsDbContext.Customers.Include(x=>x.CustomerType).ThenInclude(x=>x.Discount).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Customer> AddAsync(Customer customer)
        {
            
             var createdCustomer= await _shopRUsDbContext.AddAsync(customer);
            await  _shopRUsDbContext.SaveChangesAsync();
            return createdCustomer.Entity;
        }

        public async Task UpdateAsync(Customer customer)
        {
            _shopRUsDbContext.Customers.Update(customer);
            await _shopRUsDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Customer customer)
        {
            _shopRUsDbContext.Customers.Remove(customer);
            await _shopRUsDbContext.SaveChangesAsync();
        }

        public async Task<Discount> GetDiscountByCustomerId(string customerId)
        {
            return new Discount { };
        }
    }
}
