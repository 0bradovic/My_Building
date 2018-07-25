using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MOJA_ZGRADA.Model
{
    public class IssuedInvoiceModel
    {
        [Required]
        public int Invoice_Id { get; set; }

        [Required]
        public int Building_Id { get; set; }
        
        [Required]
        public double Issued_Invoice_Amount_Total { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Issued_Invoice_DateTime { get; set; }
        

    }
}
