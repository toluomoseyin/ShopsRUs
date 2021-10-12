
using Microsoft.AspNetCore.Mvc;
using Moq;
using ShopRUs.API.Controllers;
using ShopRUs.Application.DTOs;
using ShopRUs.Core.Models;
using ShopRUs.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
namespace ShopRUs.Test
{
    public class CustomerControllerTest
    {
        private readonly CustomerController _sut;
        private readonly Mock<ICustomerRepository> _customerRepository = new Mock<ICustomerRepository>();
        private readonly Mock<ICustomerTypeRepository> _customerTypeRepository = new Mock<ICustomerTypeRepository>();

        public CustomerControllerTest()
        {
            _sut = new CustomerController(_customerRepository.Object, _customerTypeRepository.Object);
        }

        [Fact]
        public async Task GetCustomerByIdTest()
        {
            //Arrange
            var Id = 1;
            var customer = new Customer
            {
                Id = Id,
                FirstName = "Tolu",
            };
            _customerRepository.Setup(x => x.GetByIdAsync(Id)).ReturnsAsync(customer);

            // Act
            var returnedCustomer = await _sut.GetCustomerById(Id);
            var customer1 = (returnedCustomer.Result as OkObjectResult).Value as Customer;
            // var i = returnedCustomer.Value.Id;
            //Assert
            Assert.Equal(customer1.Id, Id);

        }

        [Fact]
        public async Task GetCustomerByNameTest()
        {
            //Arrange
            var Name = "Tolu";
            var customer = new Customer
            {
                Id = 1,
                FirstName = Name,
                LastName="Omoseyin"

            };
            _customerRepository.Setup(x => x.GetCustomerByName(Name)).ReturnsAsync(customer);

            // Act
            var returnedCustomer = await _sut.GetCustomerByName(Name);
            var customer1 = (returnedCustomer.Result as OkObjectResult).Value as Customer;
            
            //Assert
            Assert.Equal(customer1.FirstName, Name);


        }


        [Fact]
        public async Task CreateCustomerTest()
        {
            //Arrange
            var CustomerTypes = new List<CustomerType>()
            {
                new CustomerType
                {
                    Id=1,
                    Type="Afilliated"
                },
                new CustomerType
                {
                    Id=2,
                    Type="Employee"
                }
            };
           
            var customerDto  = new CustomerDto
            {
               
                FirstName = "Tolu",
                LastName = "Omoseyin",
                Email="toluomoseyin01@gmail.com"

            };

            var customer = new Customer
            {
                Id = 1,
                FirstName = customerDto.FirstName,
                LastName = customerDto.LastName,
                Email = customerDto.Email
            };

            _customerTypeRepository.Setup(x => x.GetAll()).ReturnsAsync(CustomerTypes);
            _customerRepository.Setup(x => x.AddAsync(customer)).ReturnsAsync(customer);

            // Act
            var returnedCustomer = await _sut.CreateCustomer(customerDto);
            var customer1 = (returnedCustomer.Result as CreatedAtRouteResult).Value as Customer;

            //Assert
            Assert.Equal(customer1.Email, customerDto.Email);
            Assert.Equal(customer1.FirstName, customerDto.FirstName);
        }


        [Fact]
        public async Task GetCustomersTest()
        {
            //Arrange
            var Name = "Tolu";
            var customerList = new List<Customer>
            {
                new Customer{
                    Id = 1,
                FirstName = "Tolu",
                LastName = "Omoseyin"},
                 new Customer{
                    Id = 1,
                FirstName = "Tolu",
                LastName = "Omoseyin"}
            };
           
            _customerRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(customerList);

            // Act
            var returnedCustomer = await _sut.GetCustomers();
            var customer1 = (returnedCustomer.Result as OkObjectResult).Value as List<Customer>;

            //Assert
            Assert.True(customer1.Count==2);


        }
    }
}
