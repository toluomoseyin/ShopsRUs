using Microsoft.AspNetCore.Mvc;
using ShopRUs.Application.DTOs;
using ShopRUs.Core.Models;
using ShopRUs.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace ShopRUs.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        }

        [HttpPost]
        [ProducesResponseType(typeof(Customer), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Customer>> CreateCustomer([FromBody] CustomerDto customer)
        {
            var createdCustomer = new Customer
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Address = customer.Address,
                Email = customer.Email,
                IsEmployee = customer.IsEmployee,
                IsAfilliated = customer.IsAfilliated,
                PhoneNumber = customer.PhoneNumber
            };
            var returnedCustomer= await _customerRepository.AddAsync(createdCustomer);
            return CreatedAtRoute("GetCustomerByName", new { customerName = customer.FirstName }, createdCustomer);
        }


        [HttpGet("GetCustomer")]
        [ProducesResponseType(typeof(Customer), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            var customer = await _customerRepository.GetByIdAsync(id);
            return Ok(customer);
        }


        [HttpGet("{customerName}", Name = "GetCustomerByName")]
        [ProducesResponseType(typeof(Customer), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomerByName(string customerName)
        {
            var customer = await _customerRepository.GetCustomerByName(customerName);
            return Ok(customer);
        }

        [HttpGet( Name = "GetCustomers")]
        [ProducesResponseType(typeof(IEnumerable<Customer>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Customer>> GetCustomers()
        {
            var customer = await _customerRepository.GetAllAsync();
            return Ok(customer);
        }


    }
}
