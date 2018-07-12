using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System.ComponentModel.DataAnnotations;

namespace MOJA_ZGRADA.Data
{
    [Table("tbl_Invoice")]
    public class Invoice
    {
        [Key]
        public int Id { get; set; }
        
        public string Invoice_Type { get; set; }
        
        public double Invoice_Amount { get; set; }
        
    }
}
