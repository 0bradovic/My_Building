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
    [Table("tbl_Cleaning_Plan")]
    public class Cleaning_Plan
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [ForeignKey("Building")]
        public int Building_Id { get; set; }

        [Required]
        public string Cleaning_Nickname { get; set; }
        
        public string Cleaning_Type { get; set; } //1. Dezinsekcija, 2.Deritizacija, 3. Dezinsekcija i Deritizacija
        
        public double Cleaning_Price { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Cleaning_Plan_DateTime { get; set; }



        [JsonIgnore]
        public virtual ICollection<Created_Cleaning_Plan> Created_Cleaning_Plans { get; set; }
        
        [JsonIgnore]
        public virtual Building Building { get; set; }

    }
}
