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
    [Table("tbl_Cleaning_Plan")]
    public class Cleaning_Plan
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        public string Cleaning_Type { get; set; }
        
        public double Cleaning_Price { get; set; }


        public virtual ICollection<Created_Cleaning_Plan> Created_Cleaning_Plans { get; set; }

    }
}
