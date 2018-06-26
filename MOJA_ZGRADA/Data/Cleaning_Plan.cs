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
        public int Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Max 50 numbers.")]
        public string Cleaning_Type { get; set; }

        [Required]
        [StringLength(50)]
        public float Cleaning_Price { get; set; }

    }
}
