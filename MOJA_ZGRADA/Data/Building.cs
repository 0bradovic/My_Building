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
    public class Building
    {
        [Key]
        public int Id { get; set; }

        [StringLength(20, ErrorMessage = "Max 20 characters")]
        public string Nickname { get; set; }
        
        [Required]
        [DataType(DataType.Date)]
        public DateTime Date_Of_Creation { get; set; }
        
        [Required]
        [StringLength(50, ErrorMessage = "Max 50 numbers.")]
        public string Address { get; set; }
        
        [Required]
        [StringLength(3)]
        public int Number_Of_Apartments { get; set; }

        [Required]
        [StringLength(3)]
        public int Number_Of_Tenants { get; set; }

        [Required]
        [StringLength(3)]
        public int Number_Of_Parking_Places { get; set; }

        [Required]
        [StringLength(3)]
        public int Number_Of_Basements { get; set; }

        [Required]
        [StringLength(3)]
        public int Number_Of_Entrances { get; set; }

    }
}
