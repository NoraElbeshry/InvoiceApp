using InvoiceApp.Model.DTO;
using InvoiceApp.Model.Entities;
using InvoiceApp.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InvoiceApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {

        
        IUnitOfWork _unitOfWork;
        public InvoiceController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/<InvoiceController>
        [HttpGet]
        public List<InvoiceDto> Get()
        {
            var InvoicesList = _unitOfWork.Invoice.Get(i => i.Deleted !=1).Select(i => new InvoiceDto
            {
                Id = i.Id,
                ClientName = i.ClientName,
                TotalPrice = i.TotalPrice,
                CreationDate = i.CreationDate,
                ModifiedDate = i.ModifiedDate,
                DeletedDate = i.DeletedDate,
                Deleted = i.Deleted,
                InvoiceDetails = i.InvoiceDetails
            }).ToList();
            return InvoicesList;
        }

        // GET api/<InvoiceController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var Invoice = _unitOfWork.Invoice.Get(i => i.Id == id && i.Deleted !=1).Select(i => new InvoiceDto
            {
                Id = i.Id,
                ClientName = i.ClientName,
                TotalPrice = i.TotalPrice,
                CreationDate = i.CreationDate,
                ModifiedDate = i.ModifiedDate,
                DeletedDate = i.DeletedDate,
                Deleted = i.Deleted,
                InvoiceDetails = i.InvoiceDetails
            }).FirstOrDefault();
            return Ok(Invoice);
        }

        // POST api/<InvoiceController>
        [HttpPost]
        public IActionResult Post([FromBody] InvoiceDto invoiceDto)
        {
            if (invoiceDto != null)
            {
                Invoice invoice = new Invoice
                {
                    ClientName = invoiceDto.ClientName,
                    TotalPrice = invoiceDto.TotalPrice,
                    CreationDate = DateTime.Now,
                    Deleted = 0
                };
                _unitOfWork.Invoice.Add(invoice);
                _unitOfWork.SaveChanges();

                if (invoice.Id != 0)
                {
                    foreach (var item in invoiceDto.InvoiceDetails)
                    {
                        item.InvoiceId = invoice.Id;
                        _unitOfWork.InvoiceDetails.Add(item);
                    }
                }
                _unitOfWork.SaveChanges();
                return Ok(new { status = true, message = "Invoice Added Successfully" });
            }
            return Ok(new { status = false, message = "Invoice is null" });
        }

        // PUT api/<InvoiceController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] InvoiceDto invoiceDto)
        {
            if (invoiceDto != null)
            {
                Invoice invoice = _unitOfWork.Invoice.GetById(invoiceDto.Id);
                if (invoice != null)
                {
                    invoice.ClientName = invoiceDto.ClientName;
                    invoice.TotalPrice = invoiceDto.TotalPrice;
                    invoice.ModifiedDate = DateTime.Now;
                }
                _unitOfWork.Invoice.Update(invoice);
                _unitOfWork.SaveChanges();

                if (invoiceDto.InvoiceDetails.Count > 0)
                {
                    foreach (var item in invoiceDto.InvoiceDetails)
                    {
                        _unitOfWork.InvoiceDetails.Update(item);
                    }
                }
                _unitOfWork.SaveChanges();
                return Ok(new { status = true, message = "Invoice Updated Successfully" });
            }
            return Ok(new { status = false, message = "Invoice does not exist" });
        }

        // DELETE api/<InvoiceController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Invoice invoice = _unitOfWork.Invoice.GetById(id);
            if (invoice != null)
            {
                invoice.Deleted = 1;
                invoice.DeletedDate = DateTime.Now;
                _unitOfWork.Invoice.Update(invoice);
                _unitOfWork.SaveChanges();
                return Ok(new { status = true, message = "Invoice Deleted Successfully" });
            }
            return Ok(new { status = false, message = "Invoice does not exist" });
        }
    }
}
