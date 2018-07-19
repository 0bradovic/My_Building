using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MOJA_ZGRADA.Model
{
    public class RegisterModel
    {
        public string Nickname { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date_Of_Creation { get; set; }

        [Required]
        public string Address { get; set; }

        public int Number_Of_Apartments { get; set; }

        [Required]
        public int Number_Of_Tenants { get; set; }

        public int Number_Of_Parking_Places { get; set; }

        public int Number_Of_Basements { get; set; }

        public int Number_Of_Entrances { get; set; }

        public int Number_Of_Floors { get; set; }

        public bool Special_Apartments_Annotation { get; set; } = false;

        public int Admin_Id { get; set; }
    }
}
