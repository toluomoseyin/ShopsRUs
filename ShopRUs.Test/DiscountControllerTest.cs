using Microsoft.AspNetCore.Mvc;
using Moq;
using ShopRUs.API.Controllers;
using ShopRUs.Application.DTOs;
using ShopRUs.Core.Models;
using ShopRUs.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ShopRUs.Test
{
    public class DiscountControllerTest
    {
        private readonly DiscountController _sut;
        private readonly Mock<IDiscountRepository> _discountRepository = new Mock<IDiscountRepository>();

        public DiscountControllerTest()
        {
            _sut = new DiscountController(_discountRepository.Object);
        }

        [Fact]
        public async Task GetDiscountByTypeTest()
        {
            //Arrange
            var DiscountType = " Affilliated";
            var discount = new Discount
            {
                Id = 1,
                DiscountPercent = 3,
                DiscountType = DiscountType
            };
            _discountRepository.Setup(x => x.GetDiscountByType(DiscountType)).ReturnsAsync(discount);


            // Act
            var returnedCustomer = await _sut.GetDiscountByType(DiscountType);
            var customer1 = (returnedCustomer.Result as OkObjectResult).Value as Discount;


            //Assert
            Assert.Equal(DiscountType, customer1.DiscountType);

        }



      
        public async Task CreateDiscountTest()
        {
            //Arrange
          
            var discountDto = new DiscountDto
            {

                Type = "Over9YearsDiscount",
                DiscountPercent = 1.11M,
                CustomerType = "Over9YearsDiscount"

            };
            var discount = new Discount
            {
                Id = 1,
                DiscountType = "Over9YearsDiscount",
                DiscountPercent = 1.11M,
                CustomerType = new CustomerType { Type = "Over9YearsDiscount" }
            };
           
            
                         

            _discountRepository.Setup(x => x.AddAsync(discount)).ReturnsAsync(discount);

            // Act
            var returnedDiscount = await _sut.CreateDiscount(discountDto);
            var discount1 = (returnedDiscount.Result as CreatedAtRouteResult).Value as Discount;

            //Assert
            Assert.Equal(discount1.DiscountType, discountDto.CustomerType);
            Assert.Equal(discount1.DiscountPercent, discountDto.DiscountPercent);
           
        }

        [Fact]
        public async Task GetCustomersTest()
        {
            //Arrange

            var discountList = new List<Discount>
            {
                new Discount
                {
                     Id = 1,
                DiscountType = "Over9YearsDiscount",
                DiscountPercent = 1.11M,
                CustomerType = new CustomerType { Type = "Over9YearsDiscount" }
                },
                new Discount
                {
                     Id = 1,
                DiscountType = "Over9YearsDiscount",
                DiscountPercent = 1.11M,
                CustomerType = new CustomerType { Type = "Over9YearsDiscount" }
                }

            };

            _discountRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(discountList);

            // Act
            var returnedCustomer = await _sut.GetDiscounts();
            var discounts = (returnedCustomer.Result as OkObjectResult).Value as List<Discount>;

            //Assert
            Assert.True(discounts.Count == 2);


        }


    }
}
