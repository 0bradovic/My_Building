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
        public string Invoice_Type { get; set; } //1.po stanu, 2.po kvadraturi, 3.po broju stanara => [fromquery] maxamount
        
        [ForeignKey("Building")]
        public int Building_Id { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Invoice_DateTime { get; set; }

        [Required]
        public double Invoice_Amount { get; set; }

        [Required]
        public string Invoice_Bank_Account { get; set; }

        [Required]
        public string Invoice_Purpose_of_Payment { get; set; }

        [Required]
        public string Invoice_Recipient { get; set; }

        public string Invoice_Currency { get; set; } = "RSD";

        public string Invoice_Model { get; set; }

        public string Invoice_Call_to_Number { get; set; }


        [JsonIgnore]
        public virtual ICollection<Issued_Invoice> Issued_Invoices { get; set; }
        
        [JsonIgnore]
        public virtual Building Building { get; set; }

    }
}
