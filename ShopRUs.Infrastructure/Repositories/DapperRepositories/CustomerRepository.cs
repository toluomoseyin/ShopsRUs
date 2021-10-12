using Dapper;
using Microsoft.Data.Sqlite;
using ShopRUs.Application.DTOs;
using ShopRUs.Core.Models;
using ShopRUs.Core.Repositories;
using ShopRUs.Infrastructure.Seeder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopRUs.Infrastructure.Repositories.DapperRepositories
{
    public class CustomerRepository :ICustomerRepository
    {
        private readonly DatabaseConfig _databaseConfig;

        public CustomerRepository(DatabaseConfig databaseConfig)
        {
            _databaseConfig = databaseConfig ?? throw new ArgumentNullException(nameof(databaseConfig));
        }

        public async Task<Customer> AddAsync(Customer customer)
        {
            using var connection = new SqliteConnection(_databaseConfig.Name);
            var affected = await connection.ExecuteScalarAsync<Customer>("INSERT INTO Customer (FirstName, LastName, Email, PhoneNumber,Address,IsEmployee,IsAfilliated,Created_at,DiscountId) VALUES (@FirstName, @LastName, @Email,@PhoneNumber,@Address,@IsEmployee,@IsAfilliated,@Created_at,@DiscountId)",
                new { FirstName = customer.FirstName, LastName = customer.LastName, Email = customer.Email, PhoneNumber = customer.PhoneNumber, Address = customer.Address, Created_at = DateTime.Now, DiscountId = 1 });

            return affected;
        }

        //public async Task<CustomerDto> CreateCustomer(CustomerDto customer)
        //{
        //    using var connection = new SqliteConnection(_databaseConfig.Name);
        //    var affected = await connection.ExecuteScalarAsync<CustomerDto>("INSERT INTO Customer (FirstName, LastName, Email, PhoneNumber,Address,IsEmployee,IsAfilliated,Created_at,DiscountId) VALUES (@FirstName, @LastName, @Email,@PhoneNumber,@Address,@IsEmployee,@IsAfilliated,@Created_at,@DiscountId)",
        //        new { FirstName = customer.FirstName, LastName = customer.LastName, Email = customer.Email, PhoneNumber = customer.PhoneNumber, Address = customer.Address, IsEmployee = customer.IsEmployee, IsAfilliated = customer.IsAfilliated, Created_at = DateTime.Now, DiscountId = 1 });
        //    return affected;
        //}

        public Task DeleteAsync(Customer entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            using var connection = new SqliteConnection(_databaseConfig.Name);
            var customer = await connection.QueryAsync<Customer>("SELECT * FROM Customer");
            return customer;
        }

        public async Task<Customer> GetByIdAsync(int id)
        {
            using var connection = new SqliteConnection(_databaseConfig.Name);
            var customer = await connection.QueryFirstOrDefaultAsync<Customer>("SELECT * FROM Customer WHERE Id=@Id", new { Id = id });

            return customer;
        }

        //public async Task<Customer> GetCustomerById(int id)
        //{
        //    using var connection = new SqliteConnection(_databaseConfig.Name);
        //    var customer = await connection.QueryFirstOrDefaultAsync<Customer>("SELECT * FROM Customer WHERE Id=@Id", new { Id = id });

        //    return customer;
        //}

        //public async Task<Customer> GetCustomerByName(string name)
        //{
        //    using var connection = new SqliteConnection(_databaseConfig.Name);
        //    var customer = await connection.QueryFirstOrDefaultAsync<Customer>("SELECT * FROM Customer WHERE FirstName=@Name", new { Name = name });
        //    return customer;
        //}

        //public async Task<IEnumerable<Customer>> GetCustomers()
        //{
        //    using var connection = new SqliteConnection(_databaseConfig.Name);
        //    var customer = await connection.QueryAsync<Customer>("SELECT * FROM Customer");
        //    return customer;
        //}

        public Task UpdateAsync(Customer entity)
        {
            throw new NotImplementedException();
        }

        public async Task<Customer> GetCustomerByName(string name)
        {
            using var connection = new SqliteConnection(_databaseConfig.Name);
            var customer = await connection.QueryFirstAsync<Customer>("SELECT * FROM Customer WHERE FirstName=@Name", new { Name = name });
            return customer;
        }

        public Task<Discount> GetDiscountByCustomerId(string customerId)
        {
            throw new NotImplementedException();
        }
    }
}
