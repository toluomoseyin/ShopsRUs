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
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal DiscountPercent { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Modified_at { get; set; }
        public List<Customer> Customers { get; set; }
        public Discount()
        {
            Customers = new List<Customer>();
        }
    }
}
