using ShopRUs.Core.Models;
using ShopRUs.Core.Repositories;
using ShopRUs.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopRUs.Infrastructure.Repositories.EFCoreRepositories
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly ShopRUsDbContext _shopRUsDbContext;

        public InvoiceRepository(ShopRUsDbContext shopRUsDbContext)
        {
            _shopRUsDbContext = shopRUsDbContext ?? throw new ArgumentNullException(nameof(shopRUsDbContext));
        }

        public async Task<Invoice> Save(Invoice invoice)
        {
           var entityEntry = await _shopRUsDbContext.Invoices.AddAsync(invoice);
           await _shopRUsDbContext.SaveChangesAsync();
            return entityEntry.Entity;
        }
    }
}
