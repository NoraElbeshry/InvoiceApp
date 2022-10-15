using InvoiceApp.Model.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceApp.Model.DTO
{
    public class InvoiceDto
    {
        public int Id { get; set; }
        public string ClientName { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int? Deleted { get; set; }
        public List<InvoiceDetails> InvoiceDetails { get; set; }
    }
}
