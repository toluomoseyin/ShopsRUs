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
        public DateTime Created_at { get; set; }
        public DateTime Modified_at { get; set; }
        public CustomerType CustomerType { get; set; }
        public int CustomerTypeId { get; set; }
        public List<Invoice> invoices { get; set; }
        public Customer()
        {
            invoices = new List<Invoice>();
        }




    }
}
