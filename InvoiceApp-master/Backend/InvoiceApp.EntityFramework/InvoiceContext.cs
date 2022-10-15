using InvoiceApp.Model.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceApp.EntityFramework
{
    public class InvoiceContext : IdentityDbContext<IdentityUser>
    {
        public InvoiceContext(DbContextOptions<InvoiceContext> options) : base(options)
        {

        }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceDetails> InvoiceDetails { get; set; }
    }
}
