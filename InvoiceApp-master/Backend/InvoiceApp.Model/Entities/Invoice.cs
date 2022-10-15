using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceApp.Model.Entities
{
    public class Invoice
    {
        [Key]
        public int Id { get; set; }
        public string ClientName { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int? Deleted { get; set; }

        public List<InvoiceDetails> InvoiceDetails { get; set; }
    }
}
