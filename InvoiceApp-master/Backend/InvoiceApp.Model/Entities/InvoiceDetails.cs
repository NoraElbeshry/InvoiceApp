using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceApp.Model.Entities
{
    [Table("Invoice_Details")]
    public class InvoiceDetails
    {
        [Key]
        public int Id { get; set; }
        public string Item { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal ItemPrice { get; set; }  
        public int ItemAmount { get; set; }  


        public int InvoiceId { get; set; }
       
        public virtual Invoice Invoice { get; set; }
    }
}
