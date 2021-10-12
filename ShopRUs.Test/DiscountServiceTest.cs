using Microsoft.AspNetCore.Mvc;
using Moq;
using ShopRUs.Application.DTOs;
using ShopRUs.Core.Models;
using ShopRUs.Core.Repositories;
using ShopRUs.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ShopRUs.Test
{
    public class DiscountServiceTest
    {
        private readonly DiscountService _sut;
        private readonly Mock<ICustomerRepository> _customerRepository = new Mock<ICustomerRepository>();
        private readonly Mock<IDiscountRepository> _discountRepository = new Mock<IDiscountRepository>();
        private readonly Mock<IInvoiceRepository> _invoiceRepository = new Mock<IInvoiceRepository>();

        public DiscountServiceTest()
        {
            _sut = new DiscountService(_customerRepository.Object, _discountRepository.Object, _invoiceRepository.Object);
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
                   "chair,100,1,furniture",
                   "phone,100,1,electronic"
               }
            };

            var invoice = new Invoice
            {
                CustomerId = userId,
                TotalDiscountedPrice = 135,
            };
            var customer = new Customer
            {
                Id = 2,
                FirstName = "Tolu",
                LastName = "Omoseyin",
                CustomerType= new CustomerType
                {
                    Id=2,
                    Type="Employee",
                    Discount=new Discount
                    {
                        Id=2,
                        DiscountType="Employee",
                        DiscountPercent=0.3M
                    }
                }

            };
            var discount = new Discount
            {
                Id = 3,
                DiscountPercent = 0.05M,
                DiscountType = "OverTwoYears"
            };

            _customerRepository.Setup(x => x.GetByIdAsync(userId)).ReturnsAsync(customer);
            _discountRepository.Setup(x => x.GetDiscountByType("")).ReturnsAsync(discount);
            _invoiceRepository.Setup(x => x.Save(invoice)).ReturnsAsync(invoice);
           

            // Act
            var returnedCustomer = await _sut.GetDiscount(invoiceDto, userId);
           

            //Assert
            Assert.Equal(returnedCustomer.TotalDiscountedPrice, invoice.TotalDiscountedPrice);



        }
    }
}
