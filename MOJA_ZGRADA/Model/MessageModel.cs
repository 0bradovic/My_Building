using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MOJA_ZGRADA.Model
{
    public class MessageModel
    {
        [Required]
        [ForeignKey("Admin")]
        public int Admin_Id { get; set; }

        [Required]
        [ForeignKey("Tenant")]
        public int Tenant_Id { get; set; }

        [Required]
        public string Text { get; set; }

        public string File_URL { get; set; }
    }
}
