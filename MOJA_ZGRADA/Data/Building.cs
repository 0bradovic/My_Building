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
    [Table("tbl_Building")]
    public class Building
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        public string Nickname { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime Date_Of_Creation { get; set; }
        
        [Required]
        public string Address { get; set; }
        
        public int Number_Of_Apartments { get; set; }
        
        public int Number_Of_Tenants { get; set; }
        
        public int Number_Of_Parking_Places { get; set; }
        
        public int Number_Of_Basements { get; set; }
        
        public int Number_Of_Entrances { get; set; }
        
        public int Number_Of_Floors { get; set; }
        
        public bool Special_Apartments_Annotation { get; set; } = false;

        //eventulno dodati kvadraturu svih stana



        [JsonIgnore]
        public virtual ICollection<Handles> Handleses { get; set; }

        [JsonIgnore]
        public virtual ICollection<Created_Cleaning_Plan> Created_Cleaning_Plans { get; set; }

        [JsonIgnore]
        public virtual ICollection<Invoice> Invoices { get; set; }
        
        [JsonIgnore]
        public virtual ICollection<Post> Posts { get; set; }
        
    }
}
