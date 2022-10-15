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
    public class InvoiceDetailsRepository: Repository<InvoiceDetails>, IinvoiceDetailsRepository
    {
        InvoiceContext context;
        public InvoiceDetailsRepository(InvoiceContext context) : base(context)
        {
            this.context = context;
        }
    }
}
