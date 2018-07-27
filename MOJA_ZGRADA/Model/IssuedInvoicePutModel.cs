using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MOJA_ZGRADA.Model
{
    public class IssuedInvoicePutModel
    {
        public int Building_Id { get; set; }

        public double Issued_Invoice_Amount { get; set; }
        
        public bool Issued_Invoice_Payment_Done { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime Issued_Invoice_DateTime { get; set; }

        [DataType(DataType.Date)]
        public DateTime Issued_Invoice_Creation_DateTime { get; set; }

        public string Issued_Invoice_Name { get; set; }
        

    }
}
