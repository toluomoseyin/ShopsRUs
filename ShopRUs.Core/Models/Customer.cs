using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopRUs.Core.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string  Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public bool IsEmployee { get; set; }
        public bool IsAfilliated { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Modified_at { get; set; }
        public int DiscountId { get; set; }
        public Discount Discount { get; set; }

    }
}
