using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MOJA_ZGRADA.Data
{
    [Table("tbl_MessageTenant")]
    public class MessageTenant
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

        [DataType(DataType.Date)]
        public DateTime Message_Issued_DateTime { get; set; }


        [JsonIgnore]
        public virtual Admin Admin { get; set; }

        [JsonIgnore]
        public virtual Tenant Tenant { get; set; }

    }
}
