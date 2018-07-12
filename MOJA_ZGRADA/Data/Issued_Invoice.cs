﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System.ComponentModel.DataAnnotations;

namespace MOJA_ZGRADA.Data
{
    [Table("tbl_Issued_Invoice")]
    public class Issued_Invoice
    {
        [ForeignKey("Invoice")]
        public int Invoice_Id { get; set; }

        [ForeignKey("Tenant")]
        public int Tenant_Id { get; set; }

        [ForeignKey("Admin")]
        public int Admin_Id { get; set; }

        [ForeignKey("Building")]
        public int Building_Id { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime Invoice_Creation_DateTime { get; set; }

    }
}
