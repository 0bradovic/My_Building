using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace MOJA_ZGRADA.Data
{
    [Table("tbl_Issued_Invoice")]
    public class Issued_Invoice
    {
        [ForeignKey("Invoice")]
        public int Invoice_Id { get; set; }

        [ForeignKey("Tenant")]
        public int Tenant_Id { get; set; }
        
        [Required]
        public double Issued_Invoice_Amount { get; set; }
        
        [Required]
        [DataType(DataType.Date)]
        public DateTime Invoice_DateTime { get; set; }

        [DataType(DataType.Date)]
        public DateTime Invoice_Creation_DateTime { get; set; }



        [JsonIgnore]
        public virtual Invoice Invoice { get; set; }

        [JsonIgnore]
        public virtual Tenant Tenant { get; set; }
        
    }
}
