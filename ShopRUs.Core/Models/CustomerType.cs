using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopRUs.Core.Models
{
    public class CustomerType
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public Discount Discount { get; set; }
    }
}
