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
    [Table("tbl_Created_Cleaning_Plan")]
    public class Created_Cleaning_Plan
    {
        [ForeignKey("Cleaning_Plan")]
        public int Cleaning_Plan_Id { get; set; }

        [ForeignKey("Tenant")]
        public int Tenant_Id { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime Cleaning_Issued_DateTime { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime Cleaning_DateTime { get; set; }

        public bool Cleaning_Reminder { get; set; } = false;

        [DataType(DataType.Date)]
        public DateTime Cleaning_Reminder_DateTime { get; set; }




        [JsonIgnore]
        public virtual Cleaning_Plan Cleaning_Plan { get; set; }

        [JsonIgnore]
        public virtual Tenant Tenant { get; set; }
        
        
    }
}
