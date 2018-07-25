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
    [Table("tbl_Invoice")]
    public class Invoice
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [Required]
        public string Invoice_Name { get; set; }

        [Required]
        public string Invoice_Type { get; set; }
        
        [ForeignKey("Admin")]
        public int Admin_Id { get; set; }
        
        [Required]
        public string Invoice_Bank_Account { get; set; }

        [Required]
        public string Invoice_Purpose_of_Payment { get; set; }

        [Required]
        public string Invoice_Recipient { get; set; }

        public string Invoice_Currency { get; set; } = "RSD";

        public string Invoice_Model { get; set; }

        public string Invoice_Call_to_Number { get; set; }

        public double Invoice_Payment_Per_Square_Meter { get; set; } = 0;


        [JsonIgnore]
        public virtual ICollection<Issued_Invoice> Issued_Invoices { get; set; }
        
        [JsonIgnore]
        public virtual ICollection<IssuedInvoiceTenant> IssuedInvoiceTenants { get; set; }

        [JsonIgnore]
        public virtual Admin Admin { get; set; }

    }
}
