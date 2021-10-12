using ShopRUs.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopRUs.Core.Repositories
{
    public interface IInvoiceRepository
    {
        Task<Invoice> Save(Invoice invoice);
    }
}
