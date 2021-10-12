using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopRUs.Core.Models
{
    public class Discount
    {
        public int Id { get; set; }
        public string DiscountType { get; set; }
        public decimal DiscountPercent { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Modified_at { get; set; }
        public int CustomerTypeId { get; set; }
        public CustomerType CustomerType { get; set; }

    }
}
