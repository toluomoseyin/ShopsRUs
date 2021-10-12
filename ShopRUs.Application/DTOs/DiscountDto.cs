using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopRUs.Application.DTOs
{
    public class DiscountDto
    {
        public string Type { get; set; }
        public decimal DiscountPercent { get; set; }
        public string CustomerType { get; set; }
    }
}
