using InvoiceApp.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceApp.Repository
{
    public interface IUnitOfWork
    {
        IinvoiceRepository Invoice { get; }
        IinvoiceDetailsRepository InvoiceDetails { get; }
        void SaveChanges();
    }
}
