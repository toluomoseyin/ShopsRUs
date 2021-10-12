using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopRUs.Application.DTOs;
using ShopRUs.Application.Services;
using ShopRUs.Core.Models;
using ShopRUs.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopRUs.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IDiscountService _discountService;

        public InvoiceController( IDiscountService discountService)
        {

            _discountService = discountService;
        }

        [HttpPost("GetTotalAmount/{CustomerId}")]
        public async Task<ActionResult<Invoice>> GetTotalAmount([FromBody] InvoiceDto invoice, int CustomerId)
        {
            var invoiceGenerated = await  _discountService.GetDiscount(invoice, CustomerId);
            return Ok(invoiceGenerated);
        }
    }
}
