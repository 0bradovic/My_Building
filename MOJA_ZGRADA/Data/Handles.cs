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
    [Table("tbl_Handles")]
    public class Handles
    {
        [ForeignKey("Admin")]
        public int Admin_Id { get; set; }

        [ForeignKey("Building")]
        public int Building_Id { get; set; }



        [JsonIgnore]
        public virtual Admin Admin { get; set; }

        [JsonIgnore]
        public virtual Building Building { get; set; }
    }
}
