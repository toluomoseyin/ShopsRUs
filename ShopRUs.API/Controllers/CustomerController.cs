using Microsoft.AspNetCore.Mvc;
using ShopRUs.Application.DTOs;
using ShopRUs.Core.DapperRepositories;
using ShopRUs.Core.Models;
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
            await _customerRepository.CreateCustomer(customer);
            return CreatedAtRoute("GetCustomerByName", new { customerName = customer.FirstName }, customer);
        }


        [HttpGet("{Id}", Name = "GetCustomer")]
        [ProducesResponseType(typeof(Customer), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            var customer = await _customerRepository.GetCustomerById(id);
            return Ok(customer);
        }


        [HttpGet("{customerName}", Name = "GetCustomerByName")]
        [ProducesResponseType(typeof(Customer), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Customer>> GetCustomerByName(string customerName)
        {
            var customer = await _customerRepository.GetCustomerByName(customerName);
            return Ok(customer);
        }

        [HttpGet( Name = "GetCustomers")]
        [ProducesResponseType(typeof(IEnumerable<Customer>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Customer>> GetCustomers()
        {
            var customer = await _customerRepository.GetCustomers();
            return Ok(customer);
        }


    }
}
