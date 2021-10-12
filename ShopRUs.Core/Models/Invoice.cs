using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopRUs.Core.Models
{
    public class Invoice
    {
        public int Id { get; set; }
       
        public decimal TotalPrice { get; set; }
        public decimal TotalDiscountedPrice { get; set; }
        public List<ProductPurchaseDescription> ProductPurchaseDescriptions { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
       
    }
}
