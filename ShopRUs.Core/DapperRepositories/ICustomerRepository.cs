using ShopRUs.Application.DTOs;
using ShopRUs.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopRUs.Core.DapperRepositories
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetCustomers();
        Task<bool> CreateCustomer(CustomerDto customer);
        Task<Customer> GetCustomerById(int id);
        Task<Customer> GetCustomerByName(string name);


    }
}
