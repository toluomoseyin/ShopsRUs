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
        private readonly ICustomerTypeRepository _customerTypeRepository;

        public CustomerController(ICustomerRepository customerRepository, ICustomerTypeRepository customerTypeRepository)
        {
            _customerTypeRepository = customerTypeRepository ?? throw new ArgumentNullException(nameof(customerTypeRepository));
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        }

       

        [HttpPost]
        [ProducesResponseType(typeof(Customer), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Customer>> CreateCustomer([FromBody] CustomerDto customer)
        {
           
            var customerTypes =  await _customerTypeRepository.GetAll();
            var createdCustomer = new Customer();
          
            foreach (var customerType in customerTypes)
            {
                if (customer.CusomerType == customerType.Type)
                {
                    createdCustomer.CustomerTypeId = customerType.Id;
                }
            }
            createdCustomer.FirstName = customer.FirstName;
            createdCustomer.LastName = customer.LastName;
            createdCustomer.PhoneNumber = customer.PhoneNumber;
            createdCustomer.Address = customer.Address;
            createdCustomer.Email = customer.Email;
           
           
            var returnedCustomer= await _customerRepository.AddAsync(createdCustomer);
            return CreatedAtRoute("GetCustomerByName", new { customerName = customer.FirstName }, createdCustomer);
        }


        [HttpGet("GetCustomer")]
        [ProducesResponseType(typeof(Customer), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Customer>> GetCustomerById(int id)
        {
            
            var customer = await _customerRepository.GetByIdAsync(id);
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
        public async Task<ActionResult<List<Customer>>> GetCustomers()
        {
            var customer = await _customerRepository.GetAllAsync();
            return Ok(customer);
        }


    }
}
