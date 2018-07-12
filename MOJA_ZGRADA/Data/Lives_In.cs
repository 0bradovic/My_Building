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
    [Table("tbl_Lives_In")]
    public class Lives_In
    {
        [ForeignKey("Tenant")]
        public int Tenant_Id { get; set; }

        [ForeignKey("Building")]
        public int Building_Id { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime Started_Living_DateTime { get; set; }

    }
}
