using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopRUs.Application.DTOs;
using ShopRUs.Core.Models;
using ShopRUs.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ShopRUs.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountRepository _discountRepository;

        public DiscountController(IDiscountRepository discountRepository)
        {
            _discountRepository = discountRepository ?? throw new ArgumentNullException(nameof(discountRepository));
        }

        [HttpPost]
        [ProducesResponseType(typeof(Discount), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Discount>> CreateDiscount([FromBody] DiscountDto discount)
        {
            var discountCreated = new Discount
            {
                Name = discount.Name,
                Description = discount.Description,
                DiscountPercent = discount.DiscountPercent,
            };
            var returnedDiscount = await _discountRepository.AddAsync(discountCreated);
            return CreatedAtRoute("GetCustomerByName", new { customerName = discount.Name }, returnedDiscount);
        }

        [HttpGet(Name = "GetDiscounts")]
        [ProducesResponseType(typeof(IEnumerable<Discount>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Discount>> GetDiscounts()
        {
            var discount = await _discountRepository.GetAllAsync();
            return Ok(discount);
        }


        [HttpGet("{type}", Name = "GetDiscountByType")]
        [ProducesResponseType(typeof(Discount), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Discount>>> GetDiscountByType(string type)
        {
            var discounts = await _discountRepository.GetDiscountByType(type);
            return Ok(discounts);
        }

    }
}
