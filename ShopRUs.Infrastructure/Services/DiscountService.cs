using ShopRUs.Application.Common;
using ShopRUs.Application.DTOs;
using ShopRUs.Application.Services;
using ShopRUs.Core.Models;
using ShopRUs.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopRUs.Infrastructure.Services
{
    public class DiscountService: IDiscountService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IDiscountRepository _discountRepository;
        private readonly IInvoiceRepository _invoiceRepository;

        public DiscountService(ICustomerRepository customerRepository, IDiscountRepository discountRepository, IInvoiceRepository invoiceRepository)
        {
            _discountRepository = discountRepository ?? throw new ArgumentNullException(nameof(discountRepository));
            _invoiceRepository = invoiceRepository ?? throw new ArgumentNullException(nameof(invoiceRepository));
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        }

       // productname, price,quantity,category
        public async Task<Invoice> GetDiscount(InvoiceDto invoice, int userId)
        {
            decimal totalDiscountedAmount = 0;
            decimal discountPercent=0;
            var invoiceList = new List<List<string>>();
            var invoice1 = invoice.Bill;
            foreach (var item in invoice1)
            {
                invoiceList.Add(item.Split(",").ToList());
            }
            var customer = await _customerRepository.GetByIdAsync(userId);
            if (customer.CustomerType.Discount.DiscountType == "NoDiscount")
            {
                if (Utilities.get_age(customer.Created_at) >= 2)
                {
                    var twoYearsDiscount = await  _discountRepository.GetDiscountByType("OverTwoYears");
                    discountPercent = twoYearsDiscount.DiscountPercent;
                }
            }
            else
            {
                discountPercent = customer.CustomerType.Discount.DiscountPercent;
            }
            foreach (var item in invoiceList)
            {
                if (!(item[3] == "groccessorries"))
                {
                    totalDiscountedAmount += ((decimal.Parse( item[1]) * int.Parse(item[2])) * (1 - discountPercent));
                }

            }
            var totalDiscountedAmount1 = OverHumdredDiscount(totalDiscountedAmount);
            var generatedInvoice = new Invoice();
            var productPurchasedDescriptionList = new List<ProductPurchaseDescription>();
            foreach (var item in invoiceList)
            {
                productPurchasedDescriptionList.Add(new ProductPurchaseDescription
                {
                    ProductName = item[0],
                    Category = item[3],
                    Price = decimal.Parse(item[1]),
                    Quantity = int.Parse(item[2])

                });     
            }
            generatedInvoice.TotalPrice = TotalAmount(invoiceList);
            generatedInvoice.TotalDiscountedPrice = totalDiscountedAmount1;
            generatedInvoice.CustomerId = userId;
            generatedInvoice.ProductPurchaseDescriptions = productPurchasedDescriptionList;

           var returnedInvoice = await _invoiceRepository.Save(generatedInvoice);
            return returnedInvoice;
        }



        public decimal OverHumdredDiscount(decimal amount)
        {
            var noOfHundredDollar = (int)amount / 100;
            var amountOff = noOfHundredDollar * 5;
            return amount - amountOff;
        }



        public decimal TotalAmount(List<List<string>> invoice)
        {
            decimal totalAmount = 0;
            foreach (var item in invoice)
            {
                totalAmount += decimal.Parse(item[1]) * int.Parse(item[2]);
            }
            return totalAmount;
        }

       













    }
}
