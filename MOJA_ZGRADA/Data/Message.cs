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
    [Table("tbl_Message")]
    public class Message
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("Admin")]
        public int Admin_Id { get; set; }

        [ForeignKey("Tenant")]
        public int Tenant_Id { get; set; }
        
        public string Text { get; set; }

        public string File_URL { get; set; }



        [JsonIgnore]
        public virtual Admin Admin { get; set; }

        [JsonIgnore]
        public virtual Tenant Tenant { get; set; }

    }
}
