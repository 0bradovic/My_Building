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
    [Table("tbl_Tenant")]
    public class Tenant
    {
        [Key]
        public int Id { get; set; }
        
        public string First_Name { get; set; }
        
        public string Last_Name { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime Date_Of_Birth { get; set; }
        
        [RegularExpression("^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9]+)*\\.([a-z]{2,4})$", ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }
        
        public string PhoneNumber { get; set; }
        
        [StringLength(13, MinimumLength = 13, ErrorMessage = "Must have 13 numbers.")]
        public string JMBG { get; set; }
        
        public string Address { get; set; }
        
        public int Apartment_Number { get; set; }

        public string Apartment_Number_Sufixed { get; set; } = null;
        
        public int Floor_Number { get; set; }
        
        public int Number_Of_Occupants { get; set; }
        
        public string UserName { get; set; }

    }
}
