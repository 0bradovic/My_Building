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
    [Table("tbl_Tenant")]
    public class Tenant
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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

        [ForeignKey("Building")]
        public int Building_Id { get; set; }
        
        public int Apartment_Number { get; set; }

        public string Apartment_Number_Sufixed { get; set; } = null;
        
        public int Floor_Number { get; set; }
        
        public int Number_Of_Occupants { get; set; }
        
        public string UserName { get; set; }

        public int Quadrature { get; set; }
        

        [JsonIgnore]
        public virtual ICollection<Issued_Invoice> Issued_Invoices { get; set; }

        [JsonIgnore]
        public virtual ICollection<Message> Messages { get; set; }

    }
}
