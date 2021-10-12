using Microsoft.AspNetCore.Mvc;
using Moq;
using ShopRUs.API.Controllers;
using ShopRUs.Application.DTOs;
using ShopRUs.Application.Services;
using ShopRUs.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ShopRUs.Test
{
    public class InvoiceControllerTest
    {
        private readonly InvoiceController _sut;
        private readonly Mock<IDiscountService> _discountService = new Mock<IDiscountService>();

        public InvoiceControllerTest()
        {
            _sut = new InvoiceController(_discountService.Object);
        }

        [Fact]
        public async Task GetTotalAmountTest()
        {
            //Arrange

            int userId = 1;
            var invoiceDto = new InvoiceDto
            {
                Bill = new List<string>()
               {
                   "","",""
               }
            };

            var invoice = new Invoice
            {
                CustomerId=userId
            };

            _discountService.Setup(x => x.GetDiscount(invoiceDto,userId)).ReturnsAsync(invoice);

            // Act
            var returnedCustomer = await _sut.GetTotalAmount(invoiceDto,userId);
            var customer1 = (returnedCustomer.Result as OkObjectResult).Value as Invoice;

            //Assert
            Assert.Equal(userId,customer1.CustomerId);
           


        }
    }
}
