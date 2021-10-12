using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopRUs.Application.DTOs
{
    public class GeneratedInvoice
    {

        public int Id { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal TotalDiscountedPrice { get; set; }
        public string CustomerName { get; set; }
        public string Email { get; set; }
    }
}
