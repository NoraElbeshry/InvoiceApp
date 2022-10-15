using InvoiceApp.EntityFramework;
using InvoiceApp.Model.Entities;
using InvoiceApp.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceApp.Repository.Repositories
{
    public class InvoiceRepository : Repository<Invoice>, IinvoiceRepository
    {
        InvoiceContext context;
        public InvoiceRepository(InvoiceContext context) : base(context)
        {
            this.context = context;
        }
    }
}
