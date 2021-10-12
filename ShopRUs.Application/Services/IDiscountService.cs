using ShopRUs.Application.DTOs;
using ShopRUs.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopRUs.Application.Services
{
    public interface IDiscountService
    {
        Task<Invoice> GetDiscount(InvoiceDto invoice, int userId);
    }
}
