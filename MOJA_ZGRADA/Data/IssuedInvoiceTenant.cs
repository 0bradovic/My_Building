using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MOJA_ZGRADA.Data
{
    [Table("tbl_IssuedInvoiceTenant")]
    public class IssuedInvoiceTenant
    {

        [ForeignKey("Invoice")]
        public int Invoice_Id { get; set; }

        [ForeignKey("Tenant")]
        public int Tenant_Id { get; set; }

        [Required]
        public double Issued_Invoice_Amount { get; set; }

        [Required]
        public bool Issued_Invoice_Payment_Done { get; set; } = false;

        [Required]
        [DataType(DataType.Date)]
        public DateTime Issued_Invoice_DateTime { get; set; }

        [DataType(DataType.Date)]
        public DateTime Issued_Invoice_Creation_DateTime { get; set; }

        [Required]
        public string Issued_Invoice_Name { get; set; }



        [JsonIgnore]
        public virtual Invoice Invoice { get; set; }

        [JsonIgnore]
        public virtual Tenant Tenant { get; set; }
    }
}
