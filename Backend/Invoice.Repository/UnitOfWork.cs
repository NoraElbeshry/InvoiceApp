using InvoiceApp.EntityFramework;
using InvoiceApp.Repository.Interfaces;
using InvoiceApp.Repository.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceApp.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public InvoiceContext _context { get; }
        IServiceProvider _serviceProvider;
        public UnitOfWork(InvoiceContext context, IServiceProvider serviceProvider)
        {
            _context = context;
            _serviceProvider = serviceProvider;
        }

        public IinvoiceRepository Invoice => _serviceProvider.GetService<IinvoiceRepository>();
        public IinvoiceDetailsRepository InvoiceDetails => _serviceProvider.GetService<IinvoiceDetailsRepository>();

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
