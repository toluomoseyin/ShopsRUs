using Dapper;
using Microsoft.Data.Sqlite;
using ShopRUs.Application.DTOs;
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
    public class CustomerRepository : ICustomerRepository
    {
        private readonly DatabaseConfig _databaseConfig;

        public CustomerRepository(DatabaseConfig databaseConfig)
        {
            _databaseConfig = databaseConfig ?? throw new ArgumentNullException(nameof(databaseConfig));
        }

        public async Task<bool> CreateCustomer(CustomerDto customer)
        {
            using var connection = new SqliteConnection(_databaseConfig.Name);
            var affected = await connection.ExecuteAsync("INSERT INTO Customer (FirstName, LastName, Email, PhoneNumber,Address,IsEmployee,IsAfilliated,Created_at,DiscountId) VALUES (@FirstName, @LastName, @Email,@PhoneNumber,@Address,@IsEmployee,@IsAfilliated,@Created_at,@DiscountId)",
                new { FirstName = customer.FirstName, LastName = customer.LastName, Email = customer.Email, PhoneNumber=customer.PhoneNumber, Address=customer.Address, IsEmployee=customer.IsEmployee, IsAfilliated=customer.IsAfilliated, Created_at=DateTime.Now, DiscountId=1 });
            if (affected == 0)
            {
                return false;
            }
            return true;
        }

        public async Task<Customer> GetCustomerById(int id)
        {
            using var connection = new SqliteConnection(_databaseConfig.Name);
            var customer = await connection.QueryFirstOrDefaultAsync<Customer>("SELECT * FROM Customer WHERE Id=@Id", new { Id = id });

            return customer;
        }

        public async Task<Customer> GetCustomerByName(string name)
        {
            using var connection = new SqliteConnection(_databaseConfig.Name);
            var customer = await connection.QueryFirstOrDefaultAsync<Customer>("SELECT * FROM Customer WHERE FirstName=@Name", new { Name = name });
            return customer;
        }

        public async Task<IEnumerable<Customer>> GetCustomers()
        {
            using var connection = new SqliteConnection(_databaseConfig.Name);
            var customer = await connection.QueryAsync<Customer>("SELECT * FROM Customer");
            return customer;
        }
    }
}
