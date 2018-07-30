using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MOJA_ZGRADA.Model
{
    public class TenantModel
    {
        public string First_Name { get; set; }
        
        public string Last_Name { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime Date_Of_Birth { get; set; }
        
        [RegularExpression("^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9]+)*\\.([a-z]{2,4})$", ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }
        
        public string PhoneNumber { get; set; }
        
        [StringLength(13, MinimumLength = 13, ErrorMessage = "Must have 13 numbers.")]
        public string JMBG { get; set; }

        [Required]
        public int Building_Id { get; set; }
        
        //public string Address { get; set; }
        
        public int Apartment_Number { get; set; }

        public string Apartment_Number_Sufixed { get; set; } = null;

        public int Number_Of_Occupants { get; set; }
        
        public int Floor_Number { get; set; }

        public int Quadrature { get; set; }

        //[Required]
        //public string UserName { get; set; }

        //[Required]
        //public string Password { get; set; }
    }
}
